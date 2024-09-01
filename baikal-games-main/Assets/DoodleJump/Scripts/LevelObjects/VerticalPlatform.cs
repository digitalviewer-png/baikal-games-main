using System;
using UnityEngine;
using Random = UnityEngine.Random;

namespace DoodleJump
{
    public class VerticalPlatform : MonoBehaviour, IZoneObject
    {
        [SerializeField] private float _moveSpeed = 5f;
        [SerializeField] private float _moveAmplitude = 5f;

        private Zone _zone;
        private Vector3 _moveCenterPoint;
        private float _currentMoveDirection;

        public event Action ZoneLeft;

        public void SetZone(Zone zone)
        {
            _moveCenterPoint = transform.position;
            _currentMoveDirection = Random.Range(0f, 1f) > 0.5f ? -1f : 1f;
        }

        private void Update()
        {
            if (Vector3.Distance(_moveCenterPoint, transform.position) >= _moveAmplitude)
                _currentMoveDirection = -_currentMoveDirection;

            transform.position += Vector3.up * Mathf.Sign(_currentMoveDirection) * _moveSpeed * Time.deltaTime;
        }
    }
}