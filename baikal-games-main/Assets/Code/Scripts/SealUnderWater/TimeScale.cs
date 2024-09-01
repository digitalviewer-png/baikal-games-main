using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace BaikalGames.UnderwaterSealAdvancher
{
    public class TimeScale : MonoBehaviour
    {
        private float startFixedTime = 0.02f;

        public void Start()
        {
            startFixedTime = 0.02f;
            RecoverTime();
        }
        public void PauseGame()
        {
            Time.timeScale = 0.0001f;
            Time.fixedDeltaTime = 1f / 0.0001f;
        }
        public void RecoverTime()
        {
            Time.timeScale = 1f;
            Time.fixedDeltaTime = startFixedTime;
        }
    }
}
