using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BaikalGames.UnderwaterSealAdvancher
{
    public class Fish : MonoBehaviour
    {
        public enum FishType
        {
            Broadfish,
            Goby,
            Golyan,
            Omul
        }
        public FishType type;
        [SerializeField] private Animator animator;

        private void Start()
        {
            animator.SetBool("Variant", Random.Range(0, 2) == 0);
        }
    }

}
