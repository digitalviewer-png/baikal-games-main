using UnityEngine;
using UnityEngine.SceneManagement;

namespace BaikalGames.Endemics
{
    public class LevelChanger : MonoBehaviour
    {
        [SerializeField] private string levelName;

        public void ChangeScene()
        {
            SceneManager.LoadScene(levelName);
        }
    }
}
