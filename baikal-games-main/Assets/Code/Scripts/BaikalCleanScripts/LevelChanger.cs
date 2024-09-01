using UnityEngine;
using UnityEngine.SceneManagement;

namespace BaikalGames.CleanBaikal
{
    public class LevelChanger : MonoBehaviour
    {
        [SerializeField] private string levelName;

        public void LevelChange()
        {
            SceneManager.LoadScene(levelName);
        }
    }
}
