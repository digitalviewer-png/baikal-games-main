using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BaikalGames.CleanBaikal
{
    public class PauseScript : MonoBehaviour
    {
        public void OnCLickTimeStop()
        {
            Timer.timerIsRunning = false;
        }

        public void OnClickTimePlay()
        {
            Timer.timerIsRunning = true;
        }
    }
}
