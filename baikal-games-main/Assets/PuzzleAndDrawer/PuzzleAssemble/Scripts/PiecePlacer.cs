using UnityEngine;

namespace PuzzleGame
{
    public class PiecePlacer : MonoBehaviour
    {
        [SerializeField] private Camera _dragCamera;
        [SerializeField] private LayerMask _layerMask;
        [SerializeField] private LeanTouchPressAndDragInput _input;

        [Space] 
        [SerializeField] private float _placeDistance = 0.5f;
        [SerializeField] private float _placeTime = 0.5f;
        [SerializeField] private float _returnTime = 0.5f;

        private ObjectDragger<AssemblingPiece> _piecesDragger;

        private void Start()
        {
            _piecesDragger = new ObjectDragger<AssemblingPiece>(_dragCamera, _layerMask, _input);

            _piecesDragger.ObjectDragBegan += OnPieceDragStart;
            _piecesDragger.ObjectDragEnd += OnPieceDragEnd;
        }

        private void OnPieceDragStart(AssemblingPiece piece)
        {
            piece.SortingGroup.sortingOrder = 2;
        }

        private void OnPieceDragEnd(AssemblingPiece piece)
        {
            piece.SortingGroup.sortingOrder = 0;
            if (piece.IsNearRightPosition(_placeDistance))
                piece.MoveToRightPosition(_placeTime);
            else
                piece.MoveToInitialPosition(_returnTime);
        }
    }
}