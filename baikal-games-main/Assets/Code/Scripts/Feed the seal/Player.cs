using System;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
using static Unity.Collections.AllocatorManager;

namespace BaikalGames.FeedTheSeal
{
    public class Player : MonoBehaviour, IComparable
    {
        [SerializeField] private GeneratingFoodAndQarbage generatingFoodAndQarbage;

        [SerializeField] private Timer timer;

        [SerializeField] private TextMeshProUGUI pointsField;

        [SerializeField] private Animator flipperAnimator;

        [SerializeField] private string playerLocationValue;

        [SerializeField] private Sprite defaultSprite;
        [SerializeField] private Sprite winSprite;

        [SerializeField] private float clickVerificationTime = 3f;
        [SerializeField] private float timeToUnlock = 4f;

        [SerializeField] private int allowedNumberOfClicks = 3;

        [SerializeField] private Image playerImage;

        [SerializeField] SpriteRenderer flipper;

        private Animator _foodAnimator;

        private int _playerPoints = 0;
        private int _currentPlayerClicks = 0;

        private float _currentClickVerificationTime = 0;
        private float _currentTimeToUnlock = 0;

        [SerializeField] private Color blockedPlayerColor;
        [SerializeField] private Color normalPlayerColor;

        private bool _playerIsBlocked = false;

        public int PlayerPoints { get => _playerPoints; set => _playerPoints = value; }
        public Sprite DefaultSprite { get => defaultSprite; set => defaultSprite = value; }
        public Sprite WinSprite { get => winSprite; set => winSprite = value; }
        public bool PlayerIsBlocked { get => _playerIsBlocked; set => _playerIsBlocked = value; }
        public int CurrentPlayerClicks { get => _currentPlayerClicks; set => _currentPlayerClicks = value; }

        private void Start()
        {
            _currentClickVerificationTime = clickVerificationTime;
            _currentTimeToUnlock = timeToUnlock;
        }
        private void FixedUpdate()
        {
            pointsField.text = PlayerPoints.ToString();

        }
        private void Update()
        {
            if (PlayerPoints < 0) PlayerPoints = 0;
            SpamCheckTimer();
        }

        private void SpamCheckTimer()
        {
            if (_playerIsBlocked == false)
            {
                playerImage.color = normalPlayerColor;
                flipper.color = normalPlayerColor;
                _currentClickVerificationTime -= Time.deltaTime;

                if (_currentClickVerificationTime <= 0)
                {
                    _currentClickVerificationTime = clickVerificationTime;
                    _currentPlayerClicks = 0;
                }
            }
            else if (_playerIsBlocked == true)
            {
                _currentTimeToUnlock -= Time.deltaTime;
                if ( _currentTimeToUnlock <= 0)
                {
                    _playerIsBlocked = false;
                    _currentTimeToUnlock = 0;
                    _currentPlayerClicks = 0;
                    _currentTimeToUnlock = timeToUnlock;
                }
            }
        }
        private void BlockingPlayer()
        {
            _currentPlayerClicks++;

            if (_currentPlayerClicks >= allowedNumberOfClicks)
            {
                playerImage.color = blockedPlayerColor;
                flipper.color = blockedPlayerColor;
                _playerIsBlocked = true;
            }
        }

        private void ChangePlayerPosition()
        {
            if (generatingFoodAndQarbage.SpawnPosition.GetChild(0).gameObject.TryGetComponent(out _foodAnimator) && _foodAnimator.gameObject != null)
            {
                switch (playerLocationValue)
                {
                    case "LC":
                        {
                            _foodAnimator.SetTrigger("isLeftCorner");
                            break;
                        }
                    case "RC":
                        {
                            _foodAnimator.SetTrigger("isRightCorner");
                            break;
                        }
                    case "L":
                        {
                            _foodAnimator.SetTrigger("isLeft");
                            break;
                        }
                    case "R":
                        {
                            _foodAnimator.SetTrigger("isRight");
                            break;
                        }
                }
            }
        }
        public void Eat()
        {
            if (_playerIsBlocked == false) 
            {
                BlockingPlayer();

                if (generatingFoodAndQarbage.SpawnPosition.childCount != 0 && timer.CurrentTime != 0 && generatingFoodAndQarbage.RandomObject != null && generatingFoodAndQarbage.CanEat == true)
                {
                    generatingFoodAndQarbage.RandomObject = null;
                    flipperAnimator.SetTrigger("Take");
                    ChangePlayerPosition();

                    if (_foodAnimator.gameObject.layer == 6)
                    {
                        _playerPoints++;
                    }

                    else
                    {
                        _playerPoints--;
                    }

                    Destroy(_foodAnimator.gameObject, _foodAnimator.GetCurrentAnimatorStateInfo(0).length);
                    generatingFoodAndQarbage.SpawnAnimator.SetBool("spawn", false);
                }
            }
        }

        public int CompareTo(object obj)
        {
            return _playerPoints.CompareTo((obj as Player)._playerPoints);
        }
    }
}
