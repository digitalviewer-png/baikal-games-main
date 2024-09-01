using System;
using DG.Tweening;
using UnityEngine;

namespace PuzzleGame
{
    public static class LineRendererExtension
    {
        public static Tween DOPath(this LineRenderer lineRenderer, Vector3[] targetPath, float duration)
        {
            if (lineRenderer.positionCount != targetPath.Length)
                throw new ArgumentException($"{nameof(targetPath)} length should be the same as {nameof(lineRenderer)}'s {nameof(lineRenderer.positionCount)}");

            var sequence = DOTween.Sequence();
            var initialPoints = new Vector3[targetPath.Length];

            var tween = DOTween.To(() => 0f, n =>
            {
                for (int i = 0; i < lineRenderer.positionCount; i++)
                {
                    lineRenderer.SetPosition(i, Vector3.Lerp(initialPoints[i], targetPath[i], n));
                }
            }, 1f, duration);
            tween.SetEase(Ease.Linear);

            sequence.AppendCallback(() => 
            {
                lineRenderer.GetPositions(initialPoints);
            });
            sequence.Append(tween);

            return sequence;
        }
    }
}