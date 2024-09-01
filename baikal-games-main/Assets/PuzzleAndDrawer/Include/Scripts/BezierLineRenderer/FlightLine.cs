using DG.Tweening;
using PathCreation;
using UnityEngine;

namespace PuzzleGame
{
    public class FlightLine : MonoBehaviour
    {
        [SerializeField] private LineRenderer _lineRenderer;
        [SerializeField] private PathCreator _pathCreator;
        [SerializeField] private int _pointsPerUnit = 10;
        [SerializeField] private float _flightTime = 1f;
        [SerializeField] private float _lineLength = 1f;

        private Tween _animation;

        [EditorButton]
        public Tween Play()
        {
            _animation?.Kill();

            _animation = DOTween.To(() => 0, dist =>
            {
                DrawLineAtSegment(dist, dist + _lineLength);
            }, _pathCreator.path.length - _lineLength, _flightTime).SetEase(Ease.OutQuad);

            return _animation;
        }

        private void DrawLineAtSegment(float startDistance, float endDistance)
        {
            var points = (int)((endDistance - startDistance) * _pointsPerUnit);
            _lineRenderer.positionCount = points;

            for (int i = 0; i < points; i++)
            {
                _lineRenderer.SetPosition(i, _pathCreator.path.GetPointAtDistance(Mathf.Lerp(startDistance, endDistance, (float)i / (float)(points - 1)), EndOfPathInstruction.Stop));
            }
        }
    }
}