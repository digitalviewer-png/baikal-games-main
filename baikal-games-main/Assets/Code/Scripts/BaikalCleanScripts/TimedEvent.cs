using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace BaikalGames.CleanBaikal
{
    public class TimedEvent : MonoBehaviour
    {
        [Header("Timing (seconds)")]
        [SerializeField] private float timer = 4;
        [SerializeField] private bool enableAtStart;

        [Header("Timer Event")]
        public UnityEvent timerAction;

        void Start()
        {
            if (enableAtStart == true)
            {
                StartCoroutine("TimedEventStart");
            }
        }

        IEnumerator TimedEventStart()
        {
            yield return new WaitForSeconds(timer);
            timerAction.Invoke();
        }

        public void StartIEnumerator()
        {
            StartCoroutine("TimedEventStart");
        }

        public void StopIEnumerator()
        {
            StopCoroutine("TimedEventStart");
        }
    }
}
