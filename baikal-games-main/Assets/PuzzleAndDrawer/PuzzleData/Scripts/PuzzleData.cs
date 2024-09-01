using UnityEngine;

namespace PuzzleGame
{
    [CreateAssetMenu(menuName = "DATA/PuzzleData", fileName = "PuzzleData")]
    public class PuzzleData : ScriptableObject
    {
        [SerializeField] private Puzzle _puzzle;

        public Puzzle GetPuzzleInstance(Transform parent)
        {
            return Instantiate(_puzzle, parent);
        }
    }
}
