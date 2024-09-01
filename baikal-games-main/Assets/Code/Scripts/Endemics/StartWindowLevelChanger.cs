using UnityEngine;
using UnityEngine.SceneManagement;

namespace BaikalGames
{
    public class StartWindowLevelChanger : MonoBehaviour
    {
        [SerializeField] private string levelName;
        [SerializeField] private string multiplayerLevelName;
        [SerializeField] private StartWindow window;

        public void ChangeScene()
        {
            if (window.playersCount == 1)
            {
                SceneManager.LoadScene(levelName);
            }
            else
            {
                SceneManager.LoadScene(multiplayerLevelName);
            }
        }
    }
}
