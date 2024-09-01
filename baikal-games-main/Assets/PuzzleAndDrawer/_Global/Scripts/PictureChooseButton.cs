using UnityEngine;

namespace PuzzleGame
{
    public class PictureChooseButton : MonoBehaviour
    {
        [SerializeField] private PuzzleData _puzzleData;

        public PuzzleData PuzzleData => _puzzleData;
    }
}
