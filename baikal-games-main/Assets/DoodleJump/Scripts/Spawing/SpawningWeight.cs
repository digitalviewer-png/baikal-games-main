using System;
using UnityEngine;

namespace DoodleJump
{
    [Serializable]
    public struct SpawningWeight
    {
        [SerializeField] private AnimationCurve _weightCurve;

        public float GetChanceForCurrentProgress(float currentProgress)
        {
            return _weightCurve.Evaluate(currentProgress);
        }
    }
}