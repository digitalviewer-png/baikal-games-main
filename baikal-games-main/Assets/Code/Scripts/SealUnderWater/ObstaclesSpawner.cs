using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace BaikalGames.UnderwaterSealAdvancher
{
    public class ObstaclesSpawner : LandscapeSpawner
    {
        [SerializeField] private List<GameObject> objectsToSpawn = new List<GameObject>();
        [SerializeField] private List<GameObject> fishToSpawn = new List<GameObject>();
        [SerializeField] private float width, height;
        [SerializeField] private bool spawnFish;

        private void Spawn(GameObject toSpawn)
        {
            GameObject clone = Instantiate(toSpawn, spawnPoint, Quaternion.Euler(0, 0, 0), parent);
            Destroy(clone, timeToDestroy);
        }
        private void SpawnAtRadnomPlace(GameObject toSpawn)
        {
            Vector3 spawnPosition = new Vector3(Random.Range(transform.position.x-width, transform.position.x+width), Random.Range(-height, height), spawnPoint.z);
            GameObject clone = Instantiate(toSpawn, spawnPosition + Vector3.forward*(multiplicity/2f), Quaternion.Euler(0, 0, 0), parent);
            Destroy(clone, timeToDestroy);
        }
        override protected void SpawnRow()
        {
            List<int> spawned = new List<int>();
            for (int i = 0; i < countAtRow; i++)
            {
                int objectIndex;
                while (true)
                {
                    objectIndex = Random.Range(0, objectsToSpawn.Count);
                    if (!spawned.Contains(objectIndex)) break;
                }
                spawned.Add(objectIndex);
                Spawn(objectsToSpawn[objectIndex]);
            }
            SpawnFishRandom();
        }

        private void SpawnFishRandom()
        {
            int objectIndex = Random.Range(0, fishToSpawn.Count);
            SpawnAtRadnomPlace(fishToSpawn[objectIndex]);
        }
    }
}