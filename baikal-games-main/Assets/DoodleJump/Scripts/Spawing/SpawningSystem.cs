using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Random = UnityEngine.Random;

namespace DoodleJump
{
    public class SpawningSystem : MonoBehaviour
    {
        [SerializeField] private List<LevelObject> _levelObjects = new List<LevelObject>();
        [SerializeField] private Transform _levelParent;
        [SerializeField] private float _spawnHeightOffset = 10f;

        [Header("Platform Object")]
        [SerializeField] private List<PlatformContent> _platformObjects = new List<PlatformContent>();
        [SerializeField] private SpawningWeight _platformObjectsSpawnChance;

        [Space]
        [SerializeField] private int _maxObjectWeight = 100;

        [Space]
        [SerializeField] private RandomValue _minSpawningDistance;
        [SerializeField] private RandomValue _maxSpawningDistance;
        [SerializeField] private float _timeForMaxProgress;

        private float _startTime;
        private Zone _zone;
        private LevelObject _lastUsedObjectForSpawning;
        private List<LevelObject> _spawnedObjects = new List<LevelObject>();

        private float CurrentProgress => Mathf.Min((Time.time - _startTime) / (_startTime + _timeForMaxProgress), 1f);

        public void StartSpawn(Zone zone)
        {
            _startTime = Time.time;
            _zone = zone;
        }

        public bool HasPosssibleRespawnPoints()
        {
            foreach (var spawnedObject in _spawnedObjects)
            {
                if (spawnedObject.PossibleRespawnPoints.Count > 0)
                    return true;
            }

            return false;
        }

        public Vector3 GetLowestRespawnPoint()
        {
            foreach (var spawnedObject in _spawnedObjects)
            {
                if (spawnedObject.PossibleRespawnPoints.Count > 0)
                {
                    var lowestPoint = spawnedObject.PossibleRespawnPoints[0].position;

                    for (int i = 1; i < spawnedObject.PossibleRespawnPoints.Count; i++)
                    {
                        if (lowestPoint.y > spawnedObject.PossibleRespawnPoints[i].position.y)
                            lowestPoint = spawnedObject.PossibleRespawnPoints[i].position;
                    }

                    return lowestPoint;
                }
            }

            throw new Exception($"There is no respawn points on level. Use {nameof(HasPosssibleRespawnPoints)} method to check if point exist");
        }

        private void Update()
        {
            while (_spawnedObjects.Count == 0 || _spawnedObjects.Last().ObjectUpBorder <= _zone.ZoneDownCenter.position.y + _spawnHeightOffset)
            {
                var newElement = Instantiate(GetRandomObjectToSpawn());
                newElement.transform.parent = _levelParent;
                newElement.transform.position = _spawnedObjects.Count == 0
                    ? _levelParent.position
                    : Vector3.up * _spawnedObjects.Last().ObjectUpBorder;

                newElement.transform.position += Vector3.up * Mathf.Lerp(_minSpawningDistance.GetRandomValue(), _maxSpawningDistance.GetRandomValue(), CurrentProgress);
                newElement.SetZone(_zone);
                newElement.ZoneLeft += () =>
                {
                    Destroy(newElement.gameObject);
                    _spawnedObjects.Remove(newElement);
                };
                _spawnedObjects.Add(newElement);

                if (newElement.HasPlatformPoint())
                {
                    var spawnPlatformObject = Random.Range(0f, 1f);

                    if (spawnPlatformObject <= _platformObjectsSpawnChance.GetChanceForCurrentProgress(CurrentProgress))
                    {
                        var spawnPoint = newElement.GetRandomPlatformPoint();
                        var platformObject = GetRandomSpawnable(_platformObjects.Select(x => (IRandomSpawnable)x).ToList());

                        var newObj = Instantiate((PlatformContent)platformObject);
                        newObj.transform.parent = spawnPoint;
                        newObj.transform.localPosition = Vector3.zero;
                    }
                }
            }
        }

        private LevelObject GetRandomObjectToSpawn()
        {
            var sumWeight = 0;
            var currentSpawnObjects = new List<LevelObject>(_levelObjects);

            if (_lastUsedObjectForSpawning != null && _lastUsedObjectForSpawning.CantBeTwoInRow)
                currentSpawnObjects.Remove(_lastUsedObjectForSpawning);

            var newLevelObject = GetRandomSpawnable(currentSpawnObjects.Select(x => (IRandomSpawnable)x).ToList());
            _lastUsedObjectForSpawning = _levelObjects.Find(x => x == newLevelObject);
            return _lastUsedObjectForSpawning;
        }

        private IRandomSpawnable GetRandomSpawnable(List<IRandomSpawnable> spawnables)
        {
            var sumWeight = 0;
            var currentSpawnObjects = spawnables;

            foreach (var levelObject in _levelObjects)
            {
                if (levelObject.GetNormalizedSpawnWeight(CurrentProgress) == 0)
                    currentSpawnObjects.Remove(levelObject);
            }

            foreach (var levelObject in currentSpawnObjects)
            {
                sumWeight += (int)(_maxObjectWeight * levelObject.GetNormalizedSpawnWeight(CurrentProgress));
            }

            var randomValue = Random.Range(0, sumWeight);
            var progress = 0;

            foreach (var levelObject in currentSpawnObjects)
            {
                progress += (int)(_maxObjectWeight * levelObject.GetNormalizedSpawnWeight(CurrentProgress));
                if (randomValue <= progress)
                {
                    return levelObject;
                }
            }

            throw new Exception("Unexpected random spawning");
        }
    }
}