using System;
using UnityEngine;
using Random = UnityEngine.Random;

namespace DoodleJump
{
    [Serializable]
    public class PositionRandomizer
    {
        [SerializeField] private Transform _target;

        [Space]
        [SerializeField] private bool _randomizeVertical;
        [SerializeField] private float _verticalRange = 0f;

        [Space]
        [SerializeField] private bool _randomizeHorizontal;
        [SerializeField] private float _horizontalOffset = 0.5f;

        public void Randomize(float horizontalRangeMin, float horizontalRangeMax)
        {
            var position = _target.position;

            if (_randomizeHorizontal)
                position.x = Random.Range(horizontalRangeMin + _horizontalOffset, horizontalRangeMax - _horizontalOffset);

            if (_randomizeVertical && _verticalRange > 0f)
                position.y = Random.Range(position.y - _verticalRange * 0.5f, position.y + _verticalRange * 0.5f);

            _target.position = position;
        }
    }
}