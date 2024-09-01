using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;


namespace BaikalGames.UnderwaterSealAdvancher
{
    public class LeaderBoard : MonoBehaviour
    {
        [SerializeField] TextMeshProUGUI leaderLabel;
        private int leaderScore;

        private void Start()
        {
            leaderScore = PlayerPrefs.GetInt("HighScore_0", 0);
            leaderLabel.text = leaderScore.ToString();
        }

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.R))
            {
                PlayerPrefs.DeleteKey("HighScore_0");
                leaderLabel.text = "0";
            }
        }
    }
}
