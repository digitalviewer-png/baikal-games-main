using TMPro;
using UnityEngine;

namespace BaikalGames.FeedTheSeal
{
    public class Timer : MonoBehaviour
    {
        [SerializeField] private float initialTime = 60f;

        [SerializeField] private TextMeshProUGUI timerValue;

        [SerializeField] private Animator timerAnimator;
        [SerializeField] private float endingTime = 10f;

        private float _currentTime = 60f;

        private bool _timer;
        private bool _isTimerEnding;

        public float CurrentTime { get => _currentTime; set => _currentTime = value; }
        public float InitialTime { get => initialTime; set => initialTime = value; }
        public bool TimerEnable { get => _timer; set => _timer = value; }

        private void Start()
        {
            CurrentTime = InitialTime;
        }

        private void Update()
        {
            if (_currentTime <= endingTime && _currentTime > 0 && !_isTimerEnding)
            {
                timerAnimator.SetBool("IsEnding", true);
            }
        }

        private void FixedUpdate()
        {
            CountdownTimer();
        }

        private void CountdownTimer()
        {
            if (!_timer) return;

            if (_currentTime > 0)
            {
                _currentTime -= Time.deltaTime;
                timerValue.text = string.Format("{0:00} : {1:00}", 0, Mathf.Round(_currentTime));
            }
            else
            {
                if (_currentTime == 0) return;
                _currentTime = 0;
                timerAnimator.SetBool("IsEnding", false);
            }
        }
    }
}
