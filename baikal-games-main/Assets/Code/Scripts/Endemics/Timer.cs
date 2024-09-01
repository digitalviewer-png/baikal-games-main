using System.Collections;
using TMPro;
using UnityEngine;

namespace BaikalGames.Endemics
{
    public class Timer : MonoBehaviour
    {
        [SerializeField] public float seconds;
        
        // Update is called once per frame
        void Start()
        {
            StartCoroutine(TimerCoroutine());
        }

        public IEnumerator TimerCoroutine()
        {
            yield return new WaitForSeconds(2);
            while (true)
            {
                yield return new WaitForSeconds(1);
                ++seconds;
                this.gameObject.GetComponent<TextMeshProUGUI>().text = string.Format("{0:D2}:{1:D2}", (int)Mathf.Floor(seconds / 60), (int)Mathf.Floor(seconds % 60));
            }
        }
    }
}
