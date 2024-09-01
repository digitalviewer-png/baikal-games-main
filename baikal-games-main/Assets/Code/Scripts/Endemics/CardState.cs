using BaikalGames.Endemics;
using System.Collections;
using UnityEngine;

namespace BaikalGames
{
    public class CardState : MonoBehaviour
    {
        [SerializeField] public bool isLocked;
        [SerializeField] public bool isEndemic = false;
        [SerializeField] public bool isOpened = false;
        [SerializeField] public int cardID;

        private Animator _animator;
        private Cards _cards;

        public void Start()
        {
            _animator = GetComponent<Animator>();
            _cards = FindObjectOfType<Cards>();
        }

        public void RotareCard()
        {
            if (_cards.canChoose == false) return;
            if (isLocked || _cards.globalLock) return;     
            _animator.SetTrigger("Rotare");            
            isLocked = true;
            if (_cards.activeCard == -1)
            {
                _cards.activeCard = cardID;
                _cards.activeCardState = this;
            }
            else
            {
                if (_cards.activeCard == cardID) 
                {
                    _cards.endemicsCount--;
                    _cards.CheckEndemicsCount();
                    _cards.activeCard = -1;
                    _cards.activeCardState.isLocked = true;
                    _cards.activeCardState = null;
                    ++_cards.turns;
                    _cards.updateUI.UpdateTurns();
                    isLocked = true;
                }
                else
                {
                    StartCoroutine(HideCards());
                }
            }
           
        }

        //public IEnumerator CardInstantiate()
        //{
        //    yield return new WaitForSeconds(3);
        //    _animator.SetTrigger("Rotare");
        //    _cards.canChoose = true;
        //}
        private IEnumerator HideCards()
        {
            _cards.globalLock = true;
            yield return new WaitForSeconds(1);
            _cards.globalLock = false;
            _cards.activeCardState.isLocked = false;
            _cards.activeCardState._animator.SetTrigger("Rotare");
            _cards.activeCardState.isLocked = false;
            _cards.activeCard = -1;
            isLocked = false;
            _animator.SetTrigger("Rotare");
            ++_cards.turns;
            _cards.updateUI.UpdateTurns();
            if (_cards.multiplayer)
            {
                _cards.currentPlayer = _cards.currentPlayer == 1 ? 2 : 1;
                _cards.updateUI.ChangeTurn();
            }       
        }
    }
}
