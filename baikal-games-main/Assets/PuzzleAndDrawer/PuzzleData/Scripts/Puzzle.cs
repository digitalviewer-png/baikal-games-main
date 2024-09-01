using DG.Tweening;
using UnityEngine;

namespace PuzzleGame
{
    public class Puzzle : MonoBehaviour
    {
        [SerializeField] private WinAnimation _winAnimation;
        [SerializeField] private SpriteRenderer _mainImage;
        [SerializeField] private SpriteRenderer _frame;
        [SerializeField] private AssemblingPiece[] _assemblingPieces;
        [SerializeField] private PuzzleTutorial _puzzleTutorial;
        [SerializeField] private PuzzleDrawer _puzzleDrawer;
        [SerializeField] private GameObject[] _puzzleObjects;
        [SerializeField] private GameObject[] _drawerObjects;

        [Space]
        [SerializeField] private float _switchTime = 0.5f;

        [Space]
        [SerializeField] private TransitionAnimation _puzzleTransitionAnimation;
        [SerializeField] private TransitionAnimation _drawerTransitionAnimation;

        public AssemblingPiece[] AssemblingPieces => _assemblingPieces;
        public WinAnimation WinAnimation => _winAnimation;

        public TransitionAnimation PuzzleTransitionAnimation => _puzzleTransitionAnimation;
        public TransitionAnimation DrawerTransitionAnimation => _drawerTransitionAnimation;

        public void StartPuzzle()
        {
            _mainImage.SetColorAlpha(0f);
            _puzzleTutorial.StartTutorial();

            foreach (var piece in AssemblingPieces)
            {
                piece.PlacedRight += OnPiecePlaced;
            }

            foreach (var puzzleObject in _puzzleObjects)
            {
                puzzleObject.SetActive(true);
            }

            foreach (var drawerObject in _drawerObjects)
            {
                drawerObject.SetActive(false);
            }
        }

        public void StartDrawer()
        {
            _mainImage.SetColorAlpha(1f);

            foreach (var piece in _assemblingPieces)
            {
                piece.GetComponent<SpriteRenderer>().SetColorAlpha(0f);
            }

            _puzzleDrawer.Init();

            foreach (var puzzleObject in _puzzleObjects)
            {
                puzzleObject.SetActive(false);
            }

            foreach (var drawerObject in _drawerObjects)
            {
                drawerObject.SetActive(true);
            }
        }

        public void SetFullState()
        {
            _mainImage.DOFade(1f, _switchTime);
            //_frame.DOFade(0f, _switchTime);

            foreach (var piece in _assemblingPieces)
            {
                piece.GetComponent<SpriteRenderer>().DOFade(0f, _switchTime);
            }
        }

        private void OnPiecePlaced(AssemblingPiece piece)
        {
            _puzzleTutorial.EndTutorial();
        }
    }
}