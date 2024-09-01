using System;
using UnityEngine;

namespace DoodleJump
{
    public class PlatformContent : MonoBehaviour, IRandomSpawnable
    {
        [SerializeField] private SpawningWeight _spawningWeight;

        private Zone _zone;

        public event Action ZoneLeft;

        public float GetNormalizedSpawnWeight(float currentProgress)
        {
            return _spawningWeight.GetChanceForCurrentProgress(currentProgress);
        }
    }
}