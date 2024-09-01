using DG.Tweening;
using UnityEngine;

namespace PuzzleGame
{
    public class WinAnimation : MonoBehaviour
    {
        [SerializeField] private FlightLine[] _lines;
        [SerializeField] private ParticleSystem[] _fxs;
        [SerializeField] private Transform[] _stars;
        [SerializeField] private Transform[] _starsTargetPoints;
        [SerializeField] private Transform[] _starsStartPoints;
        [SerializeField] private float _starsFlightTime;

        private Sequence _animation;

        [EditorButton]
        public void Play()
        {
            gameObject.SetActive(true);

            _animation?.Kill();
            _animation = DOTween.Sequence();

            for (int i = 0; i < _stars.Length; i++)
            {
                _stars[i].position = _starsStartPoints[i].position;
                _animation.Join(_stars[i].DOMove(_starsTargetPoints[i].position, _starsFlightTime).SetEase(Ease.OutQuad));
            }

            foreach (var fx in _fxs)
            {
                fx.Play();
            }

            foreach (var line in _lines)
            {
                _animation.Join(line.Play());
            }

            _animation.onComplete += () => gameObject.SetActive(false);
        }
    }
}
