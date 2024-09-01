using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Events;

namespace BaikalGames.UnderwaterSealAdvancher
{
    public class Timer : MonoBehaviour
    {
        [SerializeField] TextMeshProUGUI timerText;
        [SerializeField] private int startTime;
        [SerializeField] private UnityEvent action;
        private int time;
        private IEnumerator Start()
        {
            time = startTime;
            while (time > 0)
            {
                time -= 1;
                string formatedText = string.Format("{0:00} : {1:00}", time / 60, time % 60);
                timerText.text = formatedText;
                
                yield return new WaitForSeconds(1f);
            }
            TimeEnd();
        }
        private void TimeEnd()
        {
            action.Invoke();
        }
        
    }
}
