using System;
using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Threading;
using UnityEngine;

namespace BaikalGames.FeedTheSeal
{
    public class GeneratingFoodAndQarbage : MonoBehaviour
    {
        [SerializeField] private Timer timer;
        [SerializeField] private ChangeScreen changeScreen;

        [SerializeField] private List<GameObject> gameObjects = new List<GameObject>();

        [SerializeField] private float TimeOfAppearance = 1f;
        [SerializeField] private float TimeOfDeletion = 2f;

        [SerializeField] private Transform spawnPosition;

        [SerializeField] private Animator spawnAnimator;

        private IEnumerator _randomGenerating;

        private GameObject _randomObject;

        private Animator _foodAnimator;

        private bool _gameStart;
        private bool _canEat;

        public Transform SpawnPosition { get => spawnPosition; set => spawnPosition = value; }
        public GameObject RandomObject { get => _randomObject; set => _randomObject = value; }
        public bool GameStart { get => _gameStart; set => _gameStart = value; }
        public IEnumerator RandomGenerating { get => _randomGenerating; set => _randomGenerating = value; }
        public bool CanEat { get => _canEat; set => _canEat = value; }
        public Animator SpawnAnimator { get => spawnAnimator; set => spawnAnimator = value; }

        private void Start()
        {
            RandomGenerating = StartRandomGenerating();
        }

        private void SpawnRandomObject()
        {
            _randomObject = gameObjects[UnityEngine.Random.Range(0, gameObjects.Count)];
            Instantiate(_randomObject, SpawnPosition.transform.position, Quaternion.identity, SpawnPosition);
            spawnAnimator.SetBool("spawn", true);

            if (spawnAnimator.GetCurrentAnimatorStateInfo(0).IsName("spawn"))
            {
                _canEat = false;
            }
            else
            {
                _canEat = true;
            }
        }

        private void DeleteRandomObject()
        {
            if (SpawnPosition.childCount != 0 && spawnPosition.GetChild(0).gameObject.TryGetComponent(out _foodAnimator))
            {
                _randomObject = null;

                _foodAnimator.SetTrigger("isFade");

                Destroy(_foodAnimator.gameObject, _foodAnimator.GetCurrentAnimatorStateInfo(0).length);
                spawnAnimator.SetBool("spawn", false);
            }
        }
        
        private void GameEnd()
        {
            if (timer.CurrentTime <= 0)
            {
                _randomObject = null;
                _gameStart = false;
                StopCoroutine(RandomGenerating);
                //CoroutineCleaning();
                changeScreen.VictoryScreenEnable();
            }
        }

        public IEnumerator StartRandomGenerating()
        {
            while (_gameStart == true)
            {
                yield return new WaitForSeconds(0.5f);

                GameEnd();

                SpawnRandomObject();

                yield return new WaitForSeconds(TimeOfDeletion);

                DeleteRandomObject();

                GameEnd();

                yield return new WaitForSeconds(TimeOfAppearance);

            }
        }
        public void CoroutineCleaning()
        {
            if (SpawnPosition.childCount != 0)
            {
                Destroy(SpawnPosition.GetChild(0).gameObject);
            }
        }
    }
}
