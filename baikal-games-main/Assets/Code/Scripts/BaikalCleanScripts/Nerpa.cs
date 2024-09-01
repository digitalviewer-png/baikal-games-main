using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

namespace BaikalGames.CleanBaikal
{
    public class Nerpa : MonoBehaviour
    {
        public static Nerpa instance;

        [SerializeField] private List<string> messages;
        [SerializeField] private TextMeshProUGUI messageText;
        [SerializeField] private GameObject messageUI;
        [SerializeField] private float timeToCloseMessage;
        private List<string> randomMessagers;

        private void Awake()
        {
            if (instance == null) instance = this;

            else Destroy(gameObject);
        }

        public void ShowMessage()
        {
            if (Random.value < .5f) return;
            StopAllCoroutines();
            messageUI.SetActive(true);
            if (randomMessagers == null || randomMessagers.Count == 1) randomMessagers = new List<string>(messages);
            int randomIndex = Random.Range(0, randomMessagers.Count);
            messageText.text = randomMessagers[randomIndex];
            randomMessagers.RemoveAt(randomIndex);
            StartCoroutine(DisablingMessage());
        }

        private IEnumerator DisablingMessage()
        {
            yield return new WaitForSeconds(timeToCloseMessage);
            messageUI.SetActive(false);
        }
    }
}
