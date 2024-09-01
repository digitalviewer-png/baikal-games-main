using UnityEngine;
using UnityEngine.SceneManagement;

namespace BaikalGames.Utility
{
    public class InactivityTimer : MonoBehaviour
    {
        private float _timer;
        private float _startTime = 60f;

        private void Awake()
        {
            _timer = _startTime;
        }

        private void Update()
        {
            if (Input.touchCount > 0) UpdateTimer();
            if (Input.anyKeyDown) UpdateTimer();
            _timer -= Time.unscaledDeltaTime;
            if (_timer < 0) SceneManager.LoadScene(0);
        }

        public void UpdateTimer() => _timer = _startTime;
    }
}
