using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

namespace PuzzleGame
{
    public class TransitionAnimation : MonoBehaviour
    {
        [SerializeField] private List<TransitionAnimation> _childAnimations;
        [SerializeField] private bool _moveThis = true;

        [Space] 
        [SerializeField] private float _pauseBetweenElements = 0.1f;

        private Sequence _moveSequence;
        private Vector3[] _localOffsets;

        public List<TransitionAnimation> ChildAnimations => _childAnimations;

        public void UpdatePositions()
        {
            _localOffsets = new Vector3[_childAnimations.Count];

            for (int i = 0; i < _childAnimations.Count; i++)
            {
                _localOffsets[i] = _childAnimations[i].transform.position - transform.position;
            }
        }

        [EditorButton]
        public Tween Move(Transform to, float moveTime)
        {
            _moveSequence = DOTween.Sequence();

            if (_moveThis)
                _moveSequence.Append(Move(to.position, moveTime));

            for (int i = 0; i < _childAnimations.Count; i++)
            {
                var childLocalPos = _childAnimations[i].transform.position - transform.position;
                _moveSequence.Join(_childAnimations[i].Move(to.position + childLocalPos, moveTime)
                    .SetDelay(_pauseBetweenElements * i));
            }

            return _moveSequence;
        }

        public void SetInPosition(Vector3 point)
        {
            if (_moveThis)
                transform.position = point;

            for (int i = 0; i < _childAnimations.Count; i++)
            {
                _childAnimations[i].SetInPosition(point + _localOffsets[i]);
            }
        }

        protected Tween Move(Vector3 to, float time) => transform.DOMove(to, time).SetEase(Ease.InOutQuad);

        private void OnDisable()
        {
            _moveSequence?.Kill();
        }
    }
}
