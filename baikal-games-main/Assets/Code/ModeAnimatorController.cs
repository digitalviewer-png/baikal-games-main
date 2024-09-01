using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BaikalGames.UnderwaterSealAdvancher
{
    public class ModeAnimatorController : MonoBehaviour
    {
        [SerializeField] private Animator onePlayerAnimator, twoPlayerAnimator;
        [SerializeField] private bool isOnePlayerModeActive, isTwoPlayerModeActive;

        private void Start()
        {
            SetAnimState(onePlayerAnimator, isOnePlayerModeActive);
            SetAnimState(twoPlayerAnimator, isTwoPlayerModeActive);
        }

        private void SetAnimState(Animator animator, bool state)
        {
            animator.SetBool("isActive", state);
        }
    }
}
