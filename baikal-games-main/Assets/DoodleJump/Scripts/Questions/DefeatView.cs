using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace DoodleJump
{
    public class DefeatView : MonoBehaviour
    {
        private const string ResultFormat = "{0} очков";
        private const string Percentage = "Текущий рекорд: {0} очка";

        [SerializeField] private TMP_Text _resultText;
        [SerializeField] private TMP_Text _percentageText;
        [SerializeField] private UiShowingAnimation _showingAnimation;
        [SerializeField] private Button _restartButton;
        [SerializeField] private Button _exitButton;

        public Action NextClicked;
        public Action ExitClicked;

        public void Show(int result, float percentage)
        {
            _showingAnimation.Show();

            _resultText.text = String.Format(ResultFormat, result.ToString());
            _percentageText.text = result != percentage ? String.Format(Percentage, percentage.ToString()) : "Новый рекорд!";
        }

        private void OnEnable()
        {
            _restartButton.onClick.AddListener(Close);
        }

        private void OnDisable()
        {
            _restartButton.onClick.RemoveListener(Close);
        }

        private void Close()
        {
            NextClicked?.Invoke();
            _showingAnimation.Hide();
        }

        private void Exit()
        {
            NextClicked?.Invoke();
        }
    }
}