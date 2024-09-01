using UnityEngine;

namespace BaikalGames.StartScreen
{
    public class GameSelectSaver : MonoBehaviour
    {
        private void Start() => GameScroll.Instance.Select(PlayerPrefs.GetInt("SelectedGameIndex", 1), false);

        public static void Save(int index) => PlayerPrefs.SetInt("SelectedGameIndex", index);
    }
}
