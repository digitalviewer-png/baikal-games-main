using System;
using UnityEngine;
using Random = UnityEngine.Random;

namespace  DoodleJump
{
    public class HorizontalPlatform : MonoBehaviour, IZoneObject
    {
        [SerializeField] private float _moveSpeed = 5f;
        [SerializeField] private BoxCollider2D _collider;

        private Zone _zone;
        private float _currentMoveDirection;

        private float PlatformHorizontalSize => _collider.size.x * transform.lossyScale.x;
        private float CurrentSidePosition => transform.position.x + Mathf.Sign(_currentMoveDirection) * PlatformHorizontalSize * 0.5f;
        
        public event Action ZoneLeft;

        public void SetZone(Zone zone)
        {
            _zone = zone;
            _currentMoveDirection = -1f + 2f * Random.Range(0, 2);
        }

        private void Update()
        {
            if (_zone.IsOutOfZoneHorizontally(CurrentSidePosition))
                _currentMoveDirection = -_currentMoveDirection;

            transform.position += Vector3.right * Mathf.Sign(_currentMoveDirection) * _moveSpeed * Time.deltaTime;
        }
    }
}
