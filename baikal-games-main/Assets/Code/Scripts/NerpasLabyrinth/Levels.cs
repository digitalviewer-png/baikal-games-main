using UnityEngine;
using UnityEngine.SceneManagement;

namespace BaikalGames.NerpasLabyrinth
{
    public class Levels : MonoBehaviour
    {
        public void Restart()
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }

        public void GoToLevel(string name)
        {
            SceneManager.LoadScene(name);
        }

        public void GoToLevel(int name)
        {
            SceneManager.LoadScene(name);
        }

        public void Quit()
        {
            SceneManager.LoadScene("AMainMenuNL");
        }
    }
}
