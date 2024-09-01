using DG.Tweening;
using UnityEngine;

namespace DoodleJump
{
    public class RotationAnimation : MonoBehaviour
    {
        [SerializeField] private Vector3 _firstKeyRotation;
        [SerializeField] private Vector3 _secondKeyRotation;
        [SerializeField] private float _loopDuration;
        [SerializeField] private Ease _ease = Ease.Linear;

        private Sequence _sequence;

        private void OnEnable()
        {
            transform.localEulerAngles = _firstKeyRotation; 

            _sequence = DOTween.Sequence();
            _sequence.Append(transform.DOLocalRotate(_secondKeyRotation, _loopDuration * 0.5f).SetEase(Ease.Linear));
            _sequence.Append(transform.DOLocalRotate(_firstKeyRotation, _loopDuration * 0.5f).SetEase(Ease.Linear));
            _sequence.SetLoops(-1);
        }

        private void OnDisable()
        {
            _sequence?.Kill();
        }
    }
}