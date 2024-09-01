using System;
using UnityEngine;

namespace DoodleJump
{
    [Serializable]
    public class Zone
    {
        [SerializeField] private float _zoneWidth;
        [SerializeField] private Transform _zoneDownCenter;

        public float LeftBorder => _zoneDownCenter.position.x - _zoneWidth * 0.5f;
        public float RightBorder => _zoneDownCenter.position.x + _zoneWidth * 0.5f;

        public Transform ZoneDownCenter => _zoneDownCenter;
        public float ZoneWidth => _zoneWidth;

        public void MoveZone(Vector3 newDownPosition)
        {
            _zoneDownCenter.position = newDownPosition;
        }

        public float LoopInsideZoneHorizontally(float xPosition)
        {
            if (xPosition < LeftBorder)
                return RightBorder;

            if (xPosition > RightBorder)
                return LeftBorder;

            return xPosition;
        }

        public bool IsOutOfZoneHorizontally(float xPosition)
        {
            return xPosition < LeftBorder || xPosition > RightBorder;
        }

        public bool IsOutOfZoneVertically(float yPosition)
        {
            return yPosition < _zoneDownCenter.position.y;
        }
    }
}
