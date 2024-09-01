using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace BaikalGames.UnderwaterSealAdvancher
{
    public class SceneSwitcher : MonoBehaviour
    {
        public void LoadSceneByName(string name) => SceneManager.LoadScene(name);
        public void LoadSceneById(int id) => SceneManager.LoadScene(id);

    }
}
