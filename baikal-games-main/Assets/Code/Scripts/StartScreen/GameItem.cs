using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace BaikalGames.StartScreen
{
    public class GameItem : MonoBehaviour
    {
        [SerializeField] private GameItem gameItemPrefab;
        [SerializeField] private Image image;
        [SerializeField] private Animator anim;
        [SerializeField] private TextMeshProUGUI text;
        [SerializeField] private int index;
        [SerializeField] private int levelIndex;
        private RectTransform _rect;

        public Image Image => image;
        public Animator Animator => anim;
        public RectTransform Target { get; set; }
        public TextMeshProUGUI Text => text;

        private void Awake()
        {
            _rect = transform as RectTransform;
        }

        private void Update()
        {
            if (Target == null) return;
            _rect.anchoredPosition = Vector2.Lerp(_rect.anchoredPosition, Target.anchoredPosition, Time.deltaTime * 6);
            _rect.sizeDelta = Vector2.Lerp(_rect.sizeDelta, Target.sizeDelta, Time.deltaTime * 6);
        }

        public void Select()
        {
            bool next = Input.mousePosition.x > Screen.width / 2;
            GameScroll.Instance.Select(index, next);
        }

        public void PlayGame()
        {
            SceneManager.LoadScene(levelIndex);
        }
        
        public void DuplicateFake(bool next)
        {
            RectTransform startTarget = GameScroll.Instance.startTarget;
            RectTransform endTarget = GameScroll.Instance.endTarget;
            GameItem duplicate = Instantiate(gameItemPrefab, _rect.parent);
            duplicate.Image.sprite = Image.sprite;
            duplicate._rect.anchoredPosition = _rect.anchoredPosition;
            duplicate._rect.sizeDelta = _rect.sizeDelta;
            duplicate.Target = next ? endTarget : startTarget;
            _rect.anchoredPosition = next ? startTarget.anchoredPosition : endTarget.anchoredPosition;
            _rect.sizeDelta = startTarget.sizeDelta;
            Destroy(duplicate.gameObject, 0.6f);
        }
    }
}
