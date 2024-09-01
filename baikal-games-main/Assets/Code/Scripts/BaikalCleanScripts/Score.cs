using System.Collections.Generic;
using TMPro;
using UnityEngine;

namespace BaikalGames.CleanBaikal
{
    public class Score : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI scoreText;

        [SerializeField] private GameObject gameWindow;
        [SerializeField] private GameObject resultPanel;
        [SerializeField] private GameObject nerpaGameWindow;
        [SerializeField] private GameObject nerpaResultPanel;

        [SerializeField] private List<GameObject> stars;

        [SerializeField] private float scorePerTrash;
        [SerializeField] private float maxScoreToWin;

        public static float count = 0;

        private void Start()
        {
            count = 0;
            gameWindow.SetActive(true);
            nerpaGameWindow.SetActive(true);
            resultPanel.SetActive(false);
            nerpaResultPanel.SetActive(false);
            TrashActivity.scorePerTrash = scorePerTrash;
        }

        private void Update()
        {
            scoreText.text = count.ToString();

            if (count == maxScoreToWin)
            {
                gameWindow.SetActive(false);
                nerpaGameWindow.SetActive(false);
                resultPanel.SetActive(true);
                nerpaResultPanel.SetActive(true);
                Timer.timerIsRunning = false;
                for (int i = 0; i < stars.Count; i++)
                {
                    stars[i].SetActive(true);
                }
            }
        }
    }
}
