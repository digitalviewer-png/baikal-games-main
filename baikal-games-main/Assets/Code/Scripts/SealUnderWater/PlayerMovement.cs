using UnityEngine;

namespace BaikalGames.UnderwaterSealAdvancher
{

    public class PlayerMovement : MonoBehaviour
    {
        [SerializeField] protected Camera mainCamera;
        [SerializeField] protected float distanceFromCamera;
        [SerializeField] protected float speed;
        [SerializeField] protected Vector3 targetFolowingPoint;
        [SerializeField] protected Vector2 mousePosition;
        [SerializeField] protected float keyboardModifier;
        protected float screenSizeMultiplier;
        protected Coroutine folowing;

        protected virtual void Start()
        {
            screenSizeMultiplier = (float)Screen.width / 1920f;
            mousePosition = new Vector2(Screen.width / 2, Screen.height / 2);
        }
        protected virtual void Update()
        {
            targetFolowingPoint = GetTargetPosition(mousePosition);
            transform.position = Vector3.Lerp(transform.position, targetFolowingPoint, speed * Time.deltaTime);
        }
        protected virtual void FixedUpdate()
        {
            for (int i = 0; i < Input.touchCount; i++)
            {
                mousePosition = Input.GetTouch(i).position;
            }
            mousePosition = mousePosition + (GetPlayerWASDInputVector() * keyboardModifier * screenSizeMultiplier);
            mousePosition = LimitMousePositionByScreenSize(mousePosition);

        }
        protected Vector2 GetPlayerWASDInputVector()
        {
            int x = 0;
            int y = 0;

            if (Input.GetKey(KeyCode.W)) y += 1;
            if (Input.GetKey(KeyCode.S)) y += -1;
            if (Input.GetKey(KeyCode.D)) x += 1;
            if (Input.GetKey(KeyCode.A)) x += -1;

            return new Vector2(x, y);
        }
        protected Vector2 LimitMousePositionByScreenSize(Vector2 mousePosition) 
            => new Vector2(Mathf.Clamp(mousePosition.x, 0, Screen.width), Mathf.Clamp(mousePosition.y, 0, Screen.height));

        protected Vector3 GetTargetPosition(Vector2 screenPosition)
        {
            Vector3 targetPoint = mainCamera.ScreenToWorldPoint(new Vector3(screenPosition.x, screenPosition.y, distanceFromCamera));
            return targetPoint;
        }
    }
}
