using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace DoodleJump
{
    public class FactPanel : MonoBehaviour
    {
        [SerializeField] private UiShowingAnimation _animation;
        [SerializeField] private TMP_Text _questionText;
        [SerializeField] private Button _nextButton;

        public void Open(string text, Action onNext)
        {
            _animation.Show();

            _nextButton.onClick.RemoveAllListeners();
            _nextButton.onClick.AddListener(() =>
            {
                _animation.Hide();
                onNext?.Invoke();
            });

            _questionText.text = text;
        }
    }
}