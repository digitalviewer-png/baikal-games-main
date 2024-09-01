using System;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

namespace DoodleJump
{
    public class LevelObject : MonoBehaviour, IZoneObject, IRandomSpawnable
    {
        [SerializeField] private SpawningWeight _spawningWeight;
        [SerializeField] private float _size;
        [SerializeField] private PositionRandomizer[] _randomObjects;
        [SerializeField] private List<Transform> _possibleRespawnPoints = new List<Transform>();
        [SerializeField] private List<Transform> _possibleBonusPoints = new List<Transform>();
        [SerializeField] private bool _cantBeTwoInRow;

        private Zone _zone;

        public event Action ZoneLeft;

        public bool CantBeTwoInRow => _cantBeTwoInRow;
        public List<Transform> PossibleRespawnPoints => _possibleRespawnPoints;
        public List<Transform> PossibleBonusPoints => _possibleBonusPoints;

        public void SetZone(Zone zone)
        {
            _zone = zone;

            foreach (var positionRandomizer in _randomObjects)
            {
                positionRandomizer.Randomize(zone.LeftBorder, zone.RightBorder);
            }

            foreach (var zoneObject in GetComponentsInChildren<IZoneObject>())
            {
                if (zoneObject != (IZoneObject)this)
                    zoneObject.SetZone(zone);
            }
        }

        public bool HasPlatformPoint()
        {
            return _possibleBonusPoints.Count > 0;
        }

        public Transform GetRandomPlatformPoint()
        {
            return _possibleBonusPoints[Random.Range(0, _possibleBonusPoints.Count)];
        }

        public float ObjectUpBorder => transform.position.y + _size;

        public float GetNormalizedSpawnWeight(float currentProgress)
        {
            return _spawningWeight.GetChanceForCurrentProgress(currentProgress);
        }

        private void OnDrawGizmos()
        {
            Gizmos.DrawLine(transform.position, transform.position + Vector3.up * _size);
        }

        private void Update()
        {
            if (_zone.IsOutOfZoneVertically(ObjectUpBorder))
                ZoneLeft?.Invoke();
        }
    }
}