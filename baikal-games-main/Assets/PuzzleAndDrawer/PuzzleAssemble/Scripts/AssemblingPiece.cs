using System;
using DG.Tweening;
using UnityEngine;
using UnityEngine.Rendering;

namespace PuzzleGame
{
    [RequireComponent(typeof(SpriteRenderer))]
    public class AssemblingPiece : MonoBehaviour
    {
        [SerializeField] private Vector3 _rightLocalPosition;
        [SerializeField] private GameObject _shadow;
        [SerializeField] private SortingGroup _sortingGroup;

        private Vector3 _initialPosition;

        public Action<AssemblingPiece> PlacedRight;

        public SortingGroup SortingGroup => _sortingGroup;

        private void Start()
        {
            _initialPosition = transform.localPosition;
        }

        public bool IsNearRightPosition(float nearDistance)
        {
            return Vector3.Distance(_rightLocalPosition, transform.localPosition) <= nearDistance;
        }

        public void MoveToRightPosition(float moveTime)
        {
            GetComponent<PolygonCollider2D>().enabled = false;
            transform.DOLocalMove(_rightLocalPosition, moveTime); 
            _shadow.SetActive(false);
             PlacedRight?.Invoke(this);
        }

        public void MoveToInitialPosition(float moveTime)
        {
            transform.DOLocalMove(_initialPosition, moveTime);
        }
    }
}