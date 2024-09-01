using System;
using DG.Tweening;
using PathCreation;
using UnityEngine;

namespace PuzzleGame
{
    public static class PathCreatorExtension
    {
        public static Tween DOBezierPath(this PathCreator pathCreator, Vector3[] points, float duration)
        {
            if (pathCreator.bezierPath.NumPoints != points.Length)
                throw new ArgumentException($"{nameof(points)} length should be the same as {nameof(pathCreator)}'s {nameof(pathCreator.bezierPath.NumPoints)}");

            var sequence = DOTween.Sequence();
            var initialPoints = new Vector3[points.Length];

            var tween = DOTween.To(() => 0f, n =>
            {
                for (int i = 0; i < pathCreator.bezierPath.NumPoints; i++)
                {
                    pathCreator.bezierPath.SetPoint(i, Vector3.Lerp(initialPoints[i], points[i], n));
                }
            }, 1f, duration);
            tween.SetEase(Ease.Linear);

            sequence.AppendCallback(() =>
            {
                for (int i = 0; i < pathCreator.bezierPath.NumPoints; i++)
                {
                    initialPoints[i] = pathCreator.bezierPath.GetPoint(i);
                }
            });
            sequence.Append(tween);

            return sequence;

        }
    }
}