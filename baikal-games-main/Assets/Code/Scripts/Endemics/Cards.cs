using System.Collections.Generic;
using UnityEngine;

namespace BaikalGames.Endemics
{
    public class Cards : MonoBehaviour
    {
        [SerializeField] public CardState activeCardState;
        [SerializeField] public UpdateUI updateUI;

        [SerializeField] public int endemicsCount;
        [SerializeField] public int activeCard;
        [SerializeField] public bool canChoose;
        [SerializeField] public bool globalLock = false;
        [SerializeField] public int baseEndemicsCount;
        [SerializeField] public bool multiplayer = false;
        [SerializeField] public int currentPlayer = 1;
        [SerializeField] public int firstPlayerScore;
        [SerializeField] public int secondPlayerScore;
        [SerializeField] public int turns;

        [SerializeField] private List<GameObject> cardsPrefabs;
        [SerializeField] private GameObject winWindow;
        [SerializeField] private GameObject winWindow2;

        private void Start()
        {
            baseEndemicsCount = endemicsCount;
            List<GameObject> cards = new List<GameObject>();
            foreach (GameObject cardPrefab in cardsPrefabs)
            {
                //if (cardPrefab.TryGetComponent<CardState>(out CardState state))
                //{
                //    endemicsCount = state.isEndemic ? ++endemicsCount : endemicsCount;
                //}
                cards.Add(cardPrefab);
                cards.Add(cardPrefab);
            }
            Shuffle(cards);
            for (int i = 0; i < cards.Count; i++)
            {
                GameObject card = Instantiate(cards[i], gameObject.transform);
                //card.GetComponent<CardState>().cardID = i+1;
                //StartCoroutine(card.GetComponent<CardState>().CardInstantiate());
            }
        }

        private void Shuffle<T>(List<T> list)
        {
            int n = list.Count;
            for (int i = 0; i < n; i++)
            {
                int rnd = Random.Range(i, n);
                T temp = list[i];
                list[i] = list[rnd];
                list[rnd] = temp;
            }
        }

        public void CheckEndemicsCount()
        {
            updateUI.UIUpdate();
            if (multiplayer)
            {
                if (currentPlayer == 1) ++firstPlayerScore;
                else ++secondPlayerScore;
                updateUI.multiplayerUIUpdate();
            }
            if (endemicsCount == 0)
            {
                updateUI.multiplayerEndGame();
                if (multiplayer)
                {
                    if (firstPlayerScore > secondPlayerScore)
                    {
                        winWindow.SetActive(true);
                    }
                    else
                    {
                        winWindow2.SetActive(true);
                    }
                }
                else
                {
                    winWindow.SetActive(true);
                }
            }
        }
    }
}
