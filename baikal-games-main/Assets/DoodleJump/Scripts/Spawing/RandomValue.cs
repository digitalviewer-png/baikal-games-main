using System;
using UnityEngine;
using Random = UnityEngine.Random;

namespace DoodleJump
{
    [Serializable]
    public struct RandomValue
    {
        [SerializeField] private float _minValue;
        [SerializeField] private float _maxValue;

        public float GetRandomValue()
        {
            return Random.Range(_minValue, _maxValue);
        }
    }
}