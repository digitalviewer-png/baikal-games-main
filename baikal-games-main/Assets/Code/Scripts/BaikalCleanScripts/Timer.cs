using System.Collections.Generic;
using TMPro;
using UnityEngine;

namespace BaikalGames.CleanBaikal
{
    public class Timer : MonoBehaviour
    {
        [SerializeField] private float timeRemaining = 60;
        [SerializeField] private float maxPoints;

        [SerializeField] private TextMeshProUGUI timeText;

        [SerializeField] private List<GameObject> stars;

        [SerializeField] private GameObject gameWindow;
        [SerializeField] private GameObject losePanel;

        public static bool timerIsRunning = false;

        private void Start()
        {
            timerIsRunning = true;
        }

        private void Update()
        {
            if (timerIsRunning)
            {
                if (timeRemaining > 0)
                {
                    timeRemaining -= Time.deltaTime;
                    DisplayTime(timeRemaining);
                }
                else
                {
                    gameWindow.SetActive(false);
                    losePanel.SetActive(true);

                    timeRemaining = 0;
                    timerIsRunning = false;
                }
            }
        }
        public void DisplayTime(float timeToDisplay)
        {
            timeToDisplay += 1;
            float minutes = Mathf.FloorToInt(timeToDisplay / 60);
            float seconds = Mathf.FloorToInt(timeToDisplay % 60);
            timeText.text = string.Format("{0:00}:{1:00}", minutes, seconds);
        }
    }
}
