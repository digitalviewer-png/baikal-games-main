using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
namespace BaikalGames.UnderwaterSealAdvancher
{
    public class LevelSelect : MonoBehaviour
    {
        public void LoadLevelByIndex(int index)
        {
            SceneManager.LoadScene(index);
        }
        public void LoadLevelByName(string name)
        {
            SceneManager.LoadScene(name);
        }
    }
}
