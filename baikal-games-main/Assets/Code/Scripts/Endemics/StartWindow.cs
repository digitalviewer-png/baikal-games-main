using UnityEngine;
using UnityEngine.UI;

namespace BaikalGames
{
    public class StartWindow : MonoBehaviour
    {
        [SerializeField] private Button onePlayer;
        [SerializeField] private Button twoPlayer;
        [SerializeField] private Animator animator;
        [SerializeField] public int playersCount = 1;

        private void Start()
        {
            onePlayer.Select();
        }


        private void Update()
        {
            if (playersCount == 1)
            {
                onePlayer.Select();                
            }
            else
            {
                twoPlayer.Select();
            }
        }

        public void ChangeToOnePlayer()
        {
            playersCount = 1;
            animator.SetBool("NerpaBool", false);
        }

        public void ChangeToTwoPlayers()
        {
            playersCount = 2;
            animator.SetBool("NerpaBool", true);
        }
    }
}
