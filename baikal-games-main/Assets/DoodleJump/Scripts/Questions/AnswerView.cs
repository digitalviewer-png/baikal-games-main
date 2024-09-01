using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace DoodleJump
{
    public class AnswerView : MonoBehaviour
    {
        [SerializeField] private TMP_Text _text;
        [SerializeField] private Button _button;
        [SerializeField] private Image _buttonBack;
        [SerializeField] private Sprite _wrongBack;
        [SerializeField] private Sprite _rightBack;

        public Action<AnswerView> Clicked;

        public void Init(string answerText)
        {
            _text.text = answerText;
        }

        public void Block()
        {
            _button.interactable = false;
        }

        public void SetRight(bool isRight)
        {
            _buttonBack.sprite = isRight ? _rightBack : _wrongBack;
        }

        private void OnEnable()
        {
            _button.onClick.AddListener(OnClick);
        }

        private void OnDisable()
        {
            _button.onClick.RemoveListener(OnClick);
        }

        private void OnClick()
        {
            Clicked?.Invoke(this);
        }
    }
}