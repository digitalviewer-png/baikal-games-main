using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace BaikalGames.UnderwaterSealAdvancher
{
    public class RaceLeader : MonoBehaviour
    {
        [SerializeField] private PlayerScore firstPlayerScore, secondPlayerScore;
        [SerializeField] private Image firstPlayerIcon, secondPlayerIcon;

        public void UpdateUI()
        {
            if (firstPlayerScore.playerScore > secondPlayerScore.playerScore)
            {
                firstPlayerIcon.enabled = true;
            }
            else if (firstPlayerScore.playerScore < secondPlayerScore.playerScore)
            {
                secondPlayerIcon.enabled = true;
            }
            else if(firstPlayerScore.playerScore == secondPlayerScore.playerScore)
            {
                firstPlayerIcon.enabled = true;
                secondPlayerIcon.enabled = true;
            }
        }
    }
}
