using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace BaikalGames.CleanBaikal
{
    public class RandomSpawnObjects : MonoBehaviour
    {
        [SerializeField] private List<GameObject> spawnObjects;
        [SerializeField] private List<Transform> spawnPoints;
        [SerializeField] private List<int> numbers;
        [SerializeField] private Transform trashCounter;
        [SerializeField] private RectTransform bounds;
        [SerializeField] private GameObject trashContainer;
        [SerializeField] private float objectsPerSpawn;
        private int _listNumber;
        private int _numberOfObject = 0;

        private void Start()
        {
            trashContainer.GetComponent<HorizontalLayoutGroup>().reverseArrangement = Convert.ToBoolean(UnityEngine.Random.Range(0, 2));

            while (trashCounter.childCount < objectsPerSpawn)
            {
                _listNumber = UnityEngine.Random.Range(0, spawnPoints.Count);
                if (!numbers.Contains(_listNumber))
                {
                    Instantiate(spawnObjects[_numberOfObject],
                        spawnPoints[_listNumber].position,
                        Quaternion.identity,
                        trashCounter);
                    _numberOfObject++;
                }
                numbers.Add(_listNumber);
            }
        }
    }
}
