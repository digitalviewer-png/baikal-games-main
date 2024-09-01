using System.Collections;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace BaikalGames.Endemics
{
    public class UpdateUI : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI endemicsCount;
        [SerializeField] private TextMeshProUGUI nerpaPhrase;
        [SerializeField] private TextMeshProUGUI firstPlayerScore;
        [SerializeField] private TextMeshProUGUI secondPlayerScore;
        [SerializeField] private TextMeshProUGUI playerWin;
        [SerializeField] private TextMeshProUGUI player2Win;
        [SerializeField] private TextMeshProUGUI turns;
        [SerializeField] private Button first;
        [SerializeField] private Button second;
        //[SerializeField] private Timer timer;
        [SerializeField] private GameObject greenTurn;
        [SerializeField] private GameObject redTurn;
        [SerializeField] private GameObject nerpaTextField;
        [SerializeField] private Cards cards;
        [SerializeField] private float nerpaTalkTime;

        public void Update()
        {
            if (!cards.multiplayer) return;
            if (cards.currentPlayer == 1) first.Select();
            else second.Select();
        }

        public void UIUpdate()
        {
            //endemicsCount.text = $"�������� ���������: {cards.endemicsCount}/{cards.baseEndemicsCount}";
        }

        public void multiplayerUIUpdate()
        {
            firstPlayerScore.text = $"{cards.firstPlayerScore}";
            secondPlayerScore.text = $"{cards.secondPlayerScore}";
        }

        public void UpdateTurns()
        {
            if (cards.multiplayer) return;
            turns.text = $"{cards.turns}  ���";
            string turnsText = "";
            if (cards.turns % 10 == 1 && cards.turns % 100 != 11)
            {
                turnsText = "���";
            }
            else if ((cards.turns % 10 >= 2 && cards.turns % 10 <= 4) && (cards.turns % 100 < 10 || cards.turns % 100 >= 20))
            {
                turnsText = "����";
            }
            else
            {
                turnsText = "�����";
            }
            playerWin.text = $"{cards.turns}  {turnsText}";
        }

        public void multiplayerEndGame()
        {
            if (cards.multiplayer)
            {
                if (cards.firstPlayerScore > cards.secondPlayerScore)
                {
                    playerWin.text = $"{cards.firstPlayerScore} �����!";
                }
                else
                {
                    player2Win.text = $"{cards.secondPlayerScore} �����!";
                }
            }
            //else
            //{
            //    string secondsText = "";

               
            //    if (timer.seconds % 10 == 1 && timer.seconds % 100 != 11)
            //    {
            //        secondsText = "�������";
            //    }
            //    else if (new[] { 2, 3, 4 }.Any(x => x == timer.seconds % 10) && !new[] { 12, 13, 14 }.Any(x => x == timer.seconds % 100))
            //    {
            //        secondsText = "�������";
            //    }
            //    else
            //    {
            //        secondsText = "������";
            //    }
            //    playerWin.text = string.Format("{0:D2}:{1:D2}", (int)Mathf.Floor(timer.seconds / 60), (int)Mathf.Floor(timer.seconds % 60));
            //    timer.StopAllCoroutines();
               
            //}
        }
        
        public void ChangeTurn()
        {
            if (cards.currentPlayer == 1)
            {
                greenTurn.SetActive(true);
                redTurn.SetActive(false);
                first.Select();
            }
            else
            {
                greenTurn.SetActive(false);
                redTurn.SetActive(true);
                second.Select();
            }
        }

        //public IEnumerator NerpaTalk(bool isEndemic)
        //{
        //    nerpaTextField.SetActive(true);
        //    nerpaPhrase.text = isEndemic ? cards.endemicsNerpaPhrases[Random.Range(0, cards.endemicsNerpaPhrases.Count)]
        //    : cards.notEndemicsNerpaPhrases[Random.Range(0, cards.notEndemicsNerpaPhrases.Count)];
        //    yield return new WaitForSeconds(nerpaTalkTime);
        //    nerpaTextField.SetActive(false);
        //    nerpaPhrase.text = "";
        //    cards.nerpaTalkCoroutine = null;
        //}
    }
}
