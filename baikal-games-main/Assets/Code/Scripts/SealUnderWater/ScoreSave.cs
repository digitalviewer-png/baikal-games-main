using System.Collections.Generic;
using UnityEngine;

namespace BaikalGames.UnderwaterSealAdvancher
{
    public class ScoreSave : MonoBehaviour
    {
        [SerializeField] private int maxSavedScores = 10;
        private List<int> savedScores = new List<int>();

        private void Start()
        {
            LoadSavedScores();
        }

        public void AddScore(int newScore)
        {
            savedScores.Add(newScore);
            savedScores.Sort((a, b) => b.CompareTo(a));


            while (savedScores.Count > maxSavedScores)
            {
                savedScores.RemoveAt(savedScores.Count - 1);
            }
            SaveScores();
        }

        private void LoadSavedScores()
        {
            savedScores.Clear();
            for (int i = 0; i < maxSavedScores; i++)
            {
                int score = PlayerPrefs.GetInt("HighScore_" + i, 0);
                if (score > 0)
                {
                    savedScores.Add(score);
                }
            }
        }

        private void SaveScores()
        {
            print("Save");
            for (int i = 0; i < savedScores.Count; i++)
            {
                PlayerPrefs.SetInt("HighScore_" + i, savedScores[i]);
                print($"HighScore_{i} : {savedScores[i]}");
            }
            PlayerPrefs.Save();
        }

        public List<int> GetValues()
        {
            return savedScores;
        }
    }
}
