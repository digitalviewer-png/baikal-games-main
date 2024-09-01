using System.Collections;
using UnityEngine;

namespace BaikalGames.UnderwaterSealAdvancher
{
    public class LandscapeSpawner : MonoBehaviour
    {
        //[SerializeField] private Transform spawnpoint;
        [SerializeField] private GameObject groundPref;
        [SerializeField] protected Transform player;
        [SerializeField] protected Transform parent;
        [SerializeField] protected float multiplicity;
        [SerializeField] protected int countSpawnRowsAtStart, countAtRow;
        [SerializeField] protected float timeToDestroy;
        [SerializeField] protected Vector3 spawnPoint;
        [SerializeField] protected bool spawnAtStart;
        protected int passedWaypoints;
        private void Awake()
        {
            for (int i = 0; i < countSpawnRowsAtStart; i++)
            {
                if(spawnAtStart)
                    SpawnRow();
                spawnPoint += multiplicity * Vector3.forward;
            }
        }
        virtual protected IEnumerator Start()
        {
            while (true)
            {
                yield return null;
                float time = player.position.z - passedWaypoints * multiplicity;
                //float time = 1/playerMovement.speed * multiplicity;
                if (time < multiplicity) continue;
                passedWaypoints++;
                SpawnRow();
                spawnPoint += multiplicity * Vector3.forward;
            }
        }
        virtual protected void SpawnRow()
        {
            Spawn(spawnPoint);
            for (int i = 1; i <= countAtRow; i++)
            {
                Vector3 targetPosition = spawnPoint + (i * multiplicity * Vector3.right);
                Spawn(targetPosition);
                targetPosition = spawnPoint + (-i * multiplicity * Vector3.right);
                Spawn(targetPosition);
            }
        }
        virtual protected void Spawn(Vector3 targetPosition)
        {
            GameObject clone = Instantiate(groundPref, targetPosition, Quaternion.Euler(0, 0, 0), parent);
            Destroy(clone, timeToDestroy);
        }

    }
}
