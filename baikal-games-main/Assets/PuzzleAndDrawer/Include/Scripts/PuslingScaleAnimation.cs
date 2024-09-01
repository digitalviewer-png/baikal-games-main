using DG.Tweening;
using UnityEngine;

namespace PuzzleGame
{
    public class PuslingScaleAnimation : MonoBehaviour
    {
        [SerializeField] private Vector3 _minScale;
        [SerializeField] private Vector3 _maxScale;
        [SerializeField] private float _loopTime = 5f;

        private Sequence _sequence;

        private void OnEnable()
        {
            transform.localScale = _minScale;
            _sequence = DOTween.Sequence();

            _sequence.Append(transform.DOScale(_maxScale, _loopTime * 0.5f).SetEase(Ease.InOutQuad));
            _sequence.Append(transform.DOScale(_minScale, _loopTime * 0.5f).SetEase(Ease.InOutQuad));
            _sequence.SetLoops(-1);
        }

        private void OnDisable()
        {
            _sequence?.Kill();
        }
    }
}