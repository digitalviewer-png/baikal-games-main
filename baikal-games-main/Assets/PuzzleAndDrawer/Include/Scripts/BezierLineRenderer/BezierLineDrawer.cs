using System;
using DG.Tweening;
using PathCreation;
using UnityEngine;

namespace PuzzleGame
{
    public class BezierLineDrawer : MonoBehaviour
    {
        [SerializeField] private LineRenderer _lineRenderer;
        [SerializeField] private PathCreator _pathCreator;
        [SerializeField] private int _pointsPerUnit = 10;
        
        [Header("Blending test")]
        [SerializeField] private Vector3[] _firstPathState;
        [SerializeField] private Vector3[] _secondPathState;
        [SerializeField] private float _blendingTime;

        private Sequence _animation;

        private void OnEnable()
        {
            _animation = DOTween.Sequence();

            SetBezierPathPoints(_firstPathState);

            _animation.Append(_pathCreator.DOBezierPath(_secondPathState, _blendingTime));
            _animation.Append(_pathCreator.DOBezierPath(_firstPathState, _blendingTime));
            _animation.onUpdate += SetPointsToLineRenderer;
            _animation.SetLoops(-1);
        }

        private void OnDisable()
        {
            _animation?.Kill();
        }

        private void SetPointsToLineRenderer()
        {
            _pathCreator.TriggerPathUpdate();
            ProcessPath(new VertexPath(_pathCreator.bezierPath, _pathCreator.transform), pointsCount =>
            {
                if (_lineRenderer.positionCount != pointsCount)
                    _lineRenderer.positionCount = pointsCount;
            }, (i, point) => _lineRenderer.SetPosition(i, point));
        }

        private void SetBezierPathPoints(Vector3[] points)
        {
            for (int i = 0; i < _pathCreator.bezierPath.NumPoints; i++)
            {
                _pathCreator.bezierPath.SetPoint(i, points[i]);
            }
        }

        #region LineSaving

        [EditorButton]
        private void CreatePath()
        {
            ProcessPath(_pathCreator.path, pointsCount => _lineRenderer.positionCount = pointsCount, (i, point) => _lineRenderer.SetPosition(i, point));
        }

        [EditorButton]
        private void SaveFirstPathPoints()
        {
            _firstPathState = new Vector3[_pathCreator.bezierPath.NumPoints];
            for (int i = 0; i < _pathCreator.bezierPath.NumPoints; i++)
            {
                _firstPathState[i] = _pathCreator.bezierPath.GetPoint(i);
            }
        }

        [EditorButton]
        private void SaveSecondPathPoints()
        {
            _secondPathState = new Vector3[_pathCreator.bezierPath.NumPoints];
            for (int i = 0; i < _pathCreator.bezierPath.NumPoints; i++)
            {
                _secondPathState[i] = _pathCreator.bezierPath.GetPoint(i);
            }
        }

        private void ProcessPath(VertexPath path, Action<int> pointsCountProcessor, Action<int, Vector3> pointSaveProcessor)
        {
            var points = (int)(path.length * _pointsPerUnit);
            pointsCountProcessor(points);
            float pointsPart = 1f / points;

            for (int i = 0; i < points; i++)
            {
                var normalized = (float)i * pointsPart;
                pointSaveProcessor(i, path.GetPointAtTime(normalized));
            }
        }

        #endregion
    }
}