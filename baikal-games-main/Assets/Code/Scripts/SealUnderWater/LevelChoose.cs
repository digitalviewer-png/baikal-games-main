using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BaikalGames.UnderwaterSealAdvancher
{
    public class LevelChoose : MonoBehaviour
    {
        [SerializeField] private Animator firstAnimator, secondAnimator;
        [SerializeField] private LevelSelect sceneSwitcher;
        private int curentLevel;

        private void Start()
        {
            curentLevel = 1;
        }
        public void SelectFirstLevel()
        {
            if (curentLevel == 1) return;
            firstAnimator.SetBool("isActive", true);
            secondAnimator.SetBool("isActive", false);
            curentLevel = 1;
        }
        public void SelectSecondLevel()
        {
            if (curentLevel == 2) return;
            firstAnimator.SetBool("isActive", false);
            secondAnimator.SetBool("isActive", true);
            curentLevel = 2;
        }
        public void Play()
        {
            sceneSwitcher.LoadLevelByIndex(curentLevel == 1 ? 6 : 7);
        }
    }
}
