using System.Collections;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace BaikalGames.NerpasLabyrinth
{
    public class Plate : MonoBehaviour, IPointerMoveHandler, IPointerEnterHandler, IPointerExitHandler, IPointerDownHandler, IPointerUpHandler
    {
        [Header("Plates Component")]
        [SerializeField] private Plates plates;

        private float _width, _height;
        float _radius;

        private Image _plateImage;

        private Coroutine _animation;

        private void Start()
        {
            Transform parent = transform.parent;

            int childCount = parent.childCount;
            int currentIndex = -1;

            for (int i = 0; i < childCount; i++)
            {
                if (parent.GetChild(i) == transform)
                {
                    currentIndex = i;
                    break;
                }
            }

            for (int i = currentIndex; i > 0; i--)
            {
                Transform child = parent.GetChild(currentIndex - 1);
                if (child.name.Contains("PlateZoneActivation")) continue;
                _plateImage = child.GetComponent<Image>();
                break;
            }
        }

        public void OnPointerMove(PointerEventData eventData)
        {
            plates.waiting.UpdateTimer();
        }

        public void OnPointerUp(PointerEventData eventData)
        {
            if (plates.plates.Count > 0 && plates.plates[plates.plates.Count - 1].CompareTag("End"))
            {
                plates.Win();
                return;
            }
            if (!plates.isCreatingLine) return;

            plates.LinesOver();
        }

        public void OnPointerDown(PointerEventData eventData)
        {
            if (plates.plates.Count > 0 && plates.plates[plates.plates.Count - 1].CompareTag("End")) return;
            if (!gameObject.CompareTag("Start")) return;
            gameObject.tag = "Plate";

            plates.isCreatingLine = true;
            AddPlate();

            _plateImage.color = new Color(_plateImage.color.r, _plateImage.color.g, _plateImage.color.b, 1);
        }



        public void OnPointerEnter(PointerEventData eventData)
        {
            if (!(gameObject.CompareTag("Plate") || gameObject.CompareTag("End"))) return;
            if (!plates.isCreatingLine) return;
            if (!ThatNextToThis()) return;
            if (HasObstacles()) return;

            if (_animation != null)
            {
                StopCoroutine(_animation);
                _animation = null;
            }
            _plateImage.color = new Color(_plateImage.color.r, _plateImage.color.g, _plateImage.color.b, 1);

            AddPlate();
        }

        public void OnPointerExit(PointerEventData eventData)
        {
            if (!plates.isCreatingLine) return;
            if (!plates.plates.Contains(this)) return;
            if (this != plates.plates[plates.plates.Count - 1]) return;
            StartAnimation();
        }

        public void LastPlate()
        {
            if (_animation != null)
            {
                StopCoroutine(_animation);
                _animation = null;
            }

            this.tag = "Start";
            _plateImage.color = new Color(_plateImage.color.r, _plateImage.color.g, _plateImage.color.b, 1);
        }

        public void StartAnimation()
        {
            _animation = StartCoroutine(LightingAnim());
        }

        private void AddPlate()
        {
            plates.plates.Add(this);
        }

        private bool ThatNextToThis()
        {
            FindSizes();

            float bias = Mathf.Min(
                ((RectTransform)transform).anchoredPosition.x / transform.position.x,
                ((RectTransform)transform).anchoredPosition.y / transform.position.y
            );

            _radius = Mathf.Max(_width, _height) / (bias * plates.radius);

            Canvas canvas = plates.actions.canvas;
            Vector2 scale = new(
                canvas.pixelRect.width / Screen.width,
                canvas.pixelRect.height / Screen.height
                );

            RectTransform thisTR = (RectTransform)this.transform;
            RectTransform thatTR = (RectTransform)plates.plates[plates.plates.Count - 1].transform;

            Vector2 thisPoint = thisTR.anchoredPosition;
            Vector2 thatPoint = thatTR.anchoredPosition;

            thisPoint = new Vector2(thisPoint.x * scale.x, thisPoint.y * scale.y);
            thatPoint = new Vector2(thatPoint.x * scale.x, thatPoint.y * scale.y);

            Vector2 deltaPoint = thisPoint - thatPoint;

            float distance = (Mathf.Sqrt(Mathf.Pow(deltaPoint.x, 2) + Mathf.Pow(deltaPoint.y, 2)) / bias);

            return distance <= _radius;
        }

        private bool HasObstacles()
        {
            Canvas canvas = plates.actions.canvas;
            Vector2 scale = new(
                canvas.pixelRect.width / Screen.width,
                canvas.pixelRect.height / Screen.height
                );

            RectTransform thisTR = (RectTransform)this.transform;
            RectTransform thatTR = (RectTransform)plates.plates[plates.plates.Count - 1].transform;

            Vector2 thisPoint = thisTR.anchoredPosition;
            Vector2 thatPoint = thatTR.anchoredPosition;

            thisPoint = new Vector2(thisPoint.x * scale.x, thisPoint.y * scale.y);
            thatPoint = new Vector2(thatPoint.x * scale.x, thatPoint.y * scale.y);

            Vector2 deltaPoint = thisPoint - thatPoint;

            int distance = (int)Mathf.Sqrt(Mathf.Pow(deltaPoint.x, 2) + Mathf.Pow(deltaPoint.y, 2));
            float bias = Mathf.Max(
                ((RectTransform)transform).anchoredPosition.x / transform.position.x,
                ((RectTransform)transform).anchoredPosition.y / transform.position.y
            );

            RaycastHit2D[] hits = Physics2D.RaycastAll(thatPoint / bias, deltaPoint.normalized, distance / bias);
            
            return hits.Length > 0;
        }

        private void FindSizes()
        {
            RectTransform rectTransform = GetComponent<RectTransform>();

            LayoutRebuilder.ForceRebuildLayoutImmediate(rectTransform);

            _width = rectTransform.rect.width;
            _height = rectTransform.rect.height;
        }

        private IEnumerator LightingAnim()
        {
            Color startColor = new Color(_plateImage.color.r, _plateImage.color.g, _plateImage.color.b, 1);
            Color endColor = new Color(startColor.r, startColor.g, startColor.b, 0.08f);

            float duration = 0.6f;
            float elapsedTime = 0f;

            while (elapsedTime < duration)
            {
                _plateImage.color = Color.Lerp(startColor, endColor, elapsedTime / duration);
                elapsedTime += Time.deltaTime;
                yield return null;
            }

            _plateImage.color = endColor;
            _animation = null;
        }
    }
}
