using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BaikalGames.FeedTheSeal
{
    public class ChangeScreen : MonoBehaviour
    {
        [SerializeField] private GeneratingFoodAndQarbage generatingFoodAndQarbage;
        [SerializeField] private VictoryResults victoryResults;

        [SerializeField] private Timer timer;

        [SerializeField] private GameObject startScreen;
        [SerializeField] private GameObject gameScreen;
        [SerializeField] private GameObject pauseScreen;
        [SerializeField] private GameObject victoryScreen;
        [SerializeField] private GameObject exitToMenuScreen;
        [SerializeField] private GameObject tutorialScreen;
        [SerializeField] private GameObject startTutorialScreen;

        private void Start()
        {
            startScreen.SetActive(true);
            startTutorialScreen.SetActive(false);
            gameScreen.SetActive(false);
            pauseScreen.SetActive(false);
            victoryScreen.SetActive(false);
        }

        private void ZeroingPoints()
        {
            generatingFoodAndQarbage.CoroutineCleaning();
            foreach (Player player in victoryResults.AllPlayers)
            {
                player.PlayerPoints = 0;
                player.PlayerIsBlocked = false;
                player.CurrentPlayerClicks = 0;
            }
        }
        public void StartTutorialScreenDisable()
        {
             startTutorialScreen.SetActive(false);
        }
        public void StartTutorialScreenEnable()
        {
            startTutorialScreen.SetActive(true);
        }
        public void GameScreenTouch()
        {
            tutorialScreen.SetActive(false);
            generatingFoodAndQarbage.GameStart = true;
            generatingFoodAndQarbage.StartCoroutine(generatingFoodAndQarbage.RandomGenerating);
            timer.TimerEnable = true;
        }
        public void StartScreenEnable()
        {
            ZeroingPoints();
            startScreen.SetActive(true);
            gameScreen.SetActive(false);
            pauseScreen.SetActive(false);
            victoryScreen.SetActive(false);
            timer.CurrentTime = timer.InitialTime;
        }
        public void StopScreenEnable()
        {
            generatingFoodAndQarbage.GameStart = false;
            generatingFoodAndQarbage.StopCoroutine(generatingFoodAndQarbage.RandomGenerating);
            timer.TimerEnable = false;
            pauseScreen.SetActive(true);
        }
        public void GameScreenEnable()
        {
            generatingFoodAndQarbage.GameStart = true;
            timer.TimerEnable = true;
            generatingFoodAndQarbage.StartCoroutine(generatingFoodAndQarbage.RandomGenerating);
            victoryScreen.SetActive(false);
            gameScreen.SetActive(true);
            startScreen.SetActive(false);
            pauseScreen.SetActive(false);
            tutorialScreen.SetActive(false);
        }
        public void VictoryScreenEnable()
        {
            victoryScreen.SetActive(true);
            victoryResults.DisplayingResults();
        }
        public void ReplayGame()
        {
            timer.CurrentTime = timer.InitialTime;
            ZeroingPoints();
            GameScreenEnable();
        }
        public void TutorialScreenEnable()
        {
            tutorialScreen.SetActive(true);
            generatingFoodAndQarbage.GameStart = false;
            generatingFoodAndQarbage.StopCoroutine(generatingFoodAndQarbage.RandomGenerating);
            timer.TimerEnable = false;
        }
    }
}
