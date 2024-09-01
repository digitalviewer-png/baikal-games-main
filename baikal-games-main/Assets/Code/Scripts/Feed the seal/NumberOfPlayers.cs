using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace BaikalGames.FeedTheSeal
{
    public class NumberOfPlayers : MonoBehaviour
    {
        [SerializeField] private VictoryResults victoryResults;

        [SerializeField] private GameObject twoPlayers;
        [SerializeField] private GameObject threePlayers;
        [SerializeField] private GameObject fourPlayers;

        [SerializeField] private GameObject twoFlipers;
        [SerializeField] private GameObject threeFlipers;
        [SerializeField] private GameObject fourFlipers;

        [SerializeField] private Button twoPlayerButton;
        [SerializeField] private Button threePlayerButton;
        [SerializeField] private Button fourPlayerButton;

        private float _currentNumberOfPlayers = 2;

        public float CurrentNumberOfPlayers { get => _currentNumberOfPlayers; set => _currentNumberOfPlayers = value; }

        private void Start()
        {
            twoPlayerButton.Select();
        }

        private void Update()
        {
            choosingNumberPlayers();
        }

        private void choosingNumberPlayers()
        {
            switch (_currentNumberOfPlayers)
            {
                case 2:
                    {
                        twoPlayerButton.Select();
                        twoPlayers.SetActive(true);
                        threePlayers.SetActive(false);
                        fourPlayers.SetActive(false);

                        twoFlipers.SetActive(true);
                        threeFlipers.SetActive(false);
                        fourFlipers.SetActive(false);

                        victoryResults.PlayerRows[0].SetActive(true);
                        victoryResults.PlayerRows[1].SetActive(true);
                        victoryResults.PlayerRows[2].SetActive(false);
                        victoryResults.PlayerRows[3].SetActive(false);

                        break;
                    }
                case 3:
                    {
                        threePlayerButton.Select();
                        twoPlayers.SetActive(false);
                        threePlayers.SetActive(true);
                        fourPlayers.SetActive(false);

                        twoFlipers.SetActive(false);
                        threeFlipers.SetActive(true);
                        fourFlipers.SetActive(false);

                        victoryResults.PlayerRows[0].SetActive(true);
                        victoryResults.PlayerRows[1].SetActive(true);
                        victoryResults.PlayerRows[2].SetActive(true);
                        victoryResults.PlayerRows[3].SetActive(false);

                        break;
                    }
                case 4:
                    {
                        fourPlayerButton.Select();
                        twoPlayers.SetActive(false);
                        threePlayers.SetActive(false);
                        fourPlayers.SetActive(true);

                        twoFlipers.SetActive(false);
                        threeFlipers.SetActive(false);
                        fourFlipers.SetActive(true);

                        victoryResults.PlayerRows[0].SetActive(true);
                        victoryResults.PlayerRows[1].SetActive(true);
                        victoryResults.PlayerRows[2].SetActive(true);
                        victoryResults.PlayerRows[3].SetActive(true);

                        break;
                    }
            }
        }

        public void ChoosingTwoPlayers()
        {
            _currentNumberOfPlayers = 2f;
        }
        public void ChoosingThreePlayers()
        {
            _currentNumberOfPlayers = 3f;
        }
        public void ChoosingFourPlayers()
        {
            _currentNumberOfPlayers = 4f;
        }
    }
}
