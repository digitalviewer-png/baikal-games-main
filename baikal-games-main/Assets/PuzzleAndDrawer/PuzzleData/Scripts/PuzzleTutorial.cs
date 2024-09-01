using DG.Tweening;
using UnityEngine;

namespace PuzzleGame
{
    public class PuzzleTutorial : MonoBehaviour
    {
        [SerializeField] private Transform _fromPoint;
        [SerializeField] private Transform _toPoint;
        [SerializeField] private float _handAnimationDuration;

        [Space]
        [SerializeField] private SpriteRenderer _selectionFrame;
        [SerializeField] private SpriteRenderer _selectionTarget;
        [SerializeField] private Transform _tutorialHand;

        private Sequence _handSequence;

        public void StartTutorial()
        {
            _tutorialHand.localPosition = _fromPoint.localPosition;

            _tutorialHand.gameObject.SetActive(true);
            _selectionFrame.DOFade(1f, 0.3f);
            _selectionTarget.DOFade(1f, 0.3f);

            var handSprite = _tutorialHand.GetComponentInChildren<SpriteRenderer>();
            handSprite.SetColorAlpha(0f);

            var boopSequence = DOTween.Sequence();
            boopSequence.Append(_tutorialHand.DOScale(0.8f, 0.3f).SetEase(Ease.Linear));
            boopSequence.Append(_tutorialHand.DOScale(1f, 0.3f).SetEase(Ease.Linear));
            boopSequence.SetEase(Ease.InOutQuad);
            
            var boopSequence2 = DOTween.Sequence();
            boopSequence2.Append(_tutorialHand.DOScale(0.8f, 0.3f).SetEase(Ease.Linear));
            boopSequence2.Append(_tutorialHand.DOScale(1f, 0.3f).SetEase(Ease.Linear));
            boopSequence2.SetEase(Ease.InOutQuad);

            _handSequence = DOTween.Sequence();

            _handSequence.Append(handSprite.DOFade(1f, 0.2f));
            _handSequence.Append(boopSequence);
            _handSequence.Append(_tutorialHand.DOLocalMove(_toPoint.localPosition, _handAnimationDuration).SetEase(Ease.Linear));
            _handSequence.Append(boopSequence2);
            _handSequence.Append(handSprite.DOFade(0f, 0.2f));

            _handSequence.SetLoops(-1);
        }

        public void EndTutorial()
        {
            _handSequence?.Kill();
            _tutorialHand.gameObject.SetActive(false);
            _selectionFrame.DOFade(0f, 0.3f);
            _selectionTarget.DOFade(0f, 0.3f);
        }
    }
}