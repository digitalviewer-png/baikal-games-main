using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

namespace DoodleJump
{
    public class FactCharacter : MonoBehaviour
    {
        [SerializeField] private SpriteRenderer[] _charRenderers;
        [SerializeField] private SpriteRenderer _factRenderer;
        [SerializeField] private List<Sprite> _possibleFacts = new List<Sprite>();
        [SerializeField] private float _appearDuration = 0.3f;

        private int _prevFact = -1;
        private Sequence _showRoutine;

        public void Show()
        {
            var randomFactIndex = 0;

            do
            {
                randomFactIndex = Random.Range(0, _possibleFacts.Count);
            } while (randomFactIndex == _prevFact);

            _factRenderer.sprite = _possibleFacts[randomFactIndex];

            _showRoutine?.Kill();
            _showRoutine = DOTween.Sequence();
            _showRoutine.Join(_factRenderer.DOFade(1f, _appearDuration));

            foreach (var spriteRenderer in _charRenderers)
            {
                _showRoutine.Join(spriteRenderer.DOFade(1f, _appearDuration));
            }
        }

        public void Hide()
        {
            _showRoutine?.Kill();
            _showRoutine = DOTween.Sequence();
            _showRoutine.Join(_factRenderer.DOFade(0f, _appearDuration));

            foreach (var spriteRenderer in _charRenderers)
            {
                _showRoutine.Join(spriteRenderer.DOFade(0f, _appearDuration));
            }
        }

        private void OnDestroy()
        {
            _showRoutine?.Kill();
        }
    }
}
