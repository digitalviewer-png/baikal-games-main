using System;
using UnityEngine;
using UnityEngine.UI;

namespace DoodleJump
{
    public class TimeIsOutPanel : MonoBehaviour
    {
        [SerializeField] private UiShowingAnimation _animation;
        [SerializeField] private Button _yesButton;
        [SerializeField] private Button _noButton;

        public void Open(Action onYes, Action onNo)
        {
            _animation.Show();

            _yesButton.onClick.RemoveAllListeners();
            _yesButton.onClick.AddListener(() =>
            {
                _animation.Hide();
                onYes?.Invoke();
            });

            _noButton.onClick.RemoveAllListeners();
            _noButton.onClick.AddListener(() =>
            {
                _animation.Hide();
                onNo?.Invoke();
            });
        }
    }
}