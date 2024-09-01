using UnityEngine;
using UnityEngine.SceneManagement;

namespace BaikalGames.Utility
{
    public class SceneChanger : MonoBehaviour
    {
        public void LoadLevel(int levelIndex)
        {
            SceneManager.LoadScene(levelIndex);
        }
    }
}
