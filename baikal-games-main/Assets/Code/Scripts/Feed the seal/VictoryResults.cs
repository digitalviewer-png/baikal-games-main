using BaikalGames.FeedTheSeal;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace BaikalGames.FeedTheSeal
{
    public class VictoryResults : MonoBehaviour
    {
        [SerializeField] private NumberOfPlayers numberOfPlayers;

        [SerializeField] private List<GameObject> playerRows;

        [SerializeField] private List<Player> playersList;

        [SerializeField] private List<Player> twoPlayers;
        [SerializeField] private List<Player> threePlayers;
        [SerializeField] private List<Player> fourPlayers;

        [SerializeField] private List<TextMeshProUGUI> playerPositions;
        [SerializeField] private List<Image> playerImages;
        [SerializeField] private List<TextMeshProUGUI> fieldsPoints;
        [SerializeField] private List<TextMeshProUGUI> pointsText;

        private float _lastPlayers;

        public List<Player> AllPlayers { get => playersList; set => playersList = value; }
        public List<GameObject> PlayerRows { get => playerRows; set => playerRows = value; }

        public void DisplayingResults()
        {
            switch (numberOfPlayers.CurrentNumberOfPlayers)
            {
                case 2:
                    {
                        twoPlayers.Sort();
                        twoPlayers.Reverse();
                        ComparisonResults(twoPlayers);
                        break;
                    }
                case 3:
                    {
                        threePlayers.Sort();
                        threePlayers.Reverse();
                        ComparisonResults(threePlayers);
                        break;
                    }
                case 4:
                    {
                        fourPlayers.Sort();
                        fourPlayers.Reverse();
                        ComparisonResults(fourPlayers);
                        break;
                    }
            }
        }

        private void ComparisonResults(List<Player> players)
        {
            _lastPlayers = 1;
            for (int i = 0; i < players.Count; i++)
            {
                if (players[i].PlayerPoints == 0)
                {
                    pointsText[i].text = "очков";
                }
                if (players[i].PlayerPoints == 1)
                {
                    pointsText[i].text = "очко";
                }
                else if (players[i].PlayerPoints >= 2 && players[i].PlayerPoints <= 4)
                {
                    pointsText[i].text = "очка";
                }
                else if (players[i].PlayerPoints >= 5 && players[i].PlayerPoints <= 20)
                {
                    pointsText[i].text = "очков";
                }

                fieldsPoints[i].text = players[i].PlayerPoints.ToString();

                playerPositions[0].text = 1.ToString();

                playerImages[0].sprite = players[0].WinSprite;

                if (i == 0) continue;

                if (players[i].PlayerPoints == players[i - 1].PlayerPoints)
                {
                    playerPositions[i].text = playerPositions[i - 1].text;

                    if (playerPositions[i].text == "1")
                    {
                        playerImages[i].sprite = players[i].WinSprite;
                    }
                    else
                    {
                        playerImages[i].sprite = players[i].DefaultSprite;
                    }
                }
                else if (players[i].PlayerPoints != players[i - 1].PlayerPoints)
                {
                    _lastPlayers++;
                    playerPositions[i].text = (_lastPlayers).ToString();
                    playerImages[i].sprite = players[i].DefaultSprite;
                }
            }
        }

    }
}
