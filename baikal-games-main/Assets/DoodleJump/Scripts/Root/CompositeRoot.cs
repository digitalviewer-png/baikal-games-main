using System.Collections;
using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace DoodleJump
{
    public class CompositeRoot : MonoBehaviour
    {
        [SerializeField] private Zone _zone;
        [SerializeField] private SpawningSystem _spawingSystem;
        [SerializeField] private Doodler _doodler;
        [SerializeField] private Timer _timer;
        [SerializeField] private ScoreForHeight _score;
        [SerializeField] private CameraControl _camera;
        [SerializeField] private GameObject _facts;
        [SerializeField] private Config _config;
        [SerializeField] private float _inputZoneSize;

        [Header("Game End")] 
        [SerializeField] private QuestionsHolder _questionsHolder;
        [SerializeField] private float _respawnHeight = 0.5f;
        [SerializeField] private float _fallTime = 2f;
        [SerializeField] private float _reloadTime = 1f;

        [Header("UI")]
        [SerializeField] private RespawnPanelView _respawnPanelView;
        [SerializeField] private FactPanel _factPanel;
        [SerializeField] private TimeIsOutPanel _timerPanel;
        [SerializeField] private TimerView _timerView;
        [SerializeField] private DefeatView _defeatView;
        [SerializeField] private TMP_Text _scoreText;
        [SerializeField] private CanvasGroup _gameplay;
        [SerializeField] private CanvasGroup _tutorial;

        private DataLoader _dataLoader;
        private LeanTouchInput _input;
        private bool _updateZone;
        private bool _restarted;
        private bool _tutorialHided;

        private void Start()
        {
            _dataLoader = new DataLoader(new GameData());

            var data = _config.Load();
            
            _inputZoneSize = data.DoodleJumpSensivity;

            _input = new LeanTouchInput(_inputZoneSize);

            _doodler.Init(_input);
            _doodler.SetZone(_zone);
            _doodler.ZoneLeft += OnDoodlerFall;

            _camera.Init(_doodler.transform);

            _score.Init(_zone.ZoneDownCenter);
            _spawingSystem.StartSpawn(_zone);

            _updateZone = true;

            _timerView.Init(_timer);
            _timer.StartTimer(60, () =>
            {
                _doodler.Rigidbody.bodyType = RigidbodyType2D.Static;
                _timerPanel.Open(() =>
                {
                    _doodler.Rigidbody.bodyType = RigidbodyType2D.Dynamic;
                }, OpenDefeatMenu);
                _timerView.gameObject.SetActive(false);
            });

            _doodler.Jump(20);
        }

        private void OnDoodlerFall()
        {
            if (_score.CurrentPoints > _dataLoader.CurrentGameData.BestResult)
            {
                _dataLoader.CurrentGameData.BestResult = _score.CurrentPoints;
                _dataLoader.SaveData();
            }

            StartCoroutine(GameEndRoutine());
        }

        private void Update()
        {
            if (_updateZone)
            {
                _zone.MoveZone(_camera.CameraViewDownPoint + Vector3.down *1f);
                _scoreText.text = _score.CurrentPoints.ToString();

                if (_tutorialHided == false)
                {
                    if (_input.GetInput() != 0)
                    {
                        _tutorialHided = true;
                        _tutorial.DOFade(0f, 0.35f).SetDelay(1f);
                    }
                }
            }
        }

        private void OnDestroy()
        {
            if (_input != null)
                _input.Dispose();
        }

        private IEnumerator GameEndRoutine()
        {
            _updateZone = false;
            _camera.SwitchState<CameraVerticalFollow>();
            _doodler.Deactivate();
            _gameplay.alpha = 0.5f;
            _facts.SetActive(false);

            _timerView.gameObject.SetActive(false);
            _timer.Pause();

            yield return new WaitForSeconds(_fallTime);

            _camera.SwitchState<CameraEmptyState>();

            yield return new WaitForSeconds(_reloadTime);

            if (_spawingSystem.HasPosssibleRespawnPoints() && _restarted == false)
            {
                var question = _questionsHolder.GetRandomQuestion();
                _respawnPanelView.Open(question);
                _respawnPanelView.Answered += OnRespawnAnswered;
            }
            else
            {
                OpenDefeatMenu();
            }
        }

        private void OnRespawnAnswered(QuestionsHolder.Question question, bool isRightAnswer)
        {
            _respawnPanelView.Close();

            if (isRightAnswer)
            {
                _restarted = true;
                _factPanel.Open(question.Fact, () =>
                {
                    if (_timer.TimeLeft.Seconds > 0)
                    {
                        _timerView.gameObject.SetActive(true);
                        _timer.Continue();
                    }

                    _gameplay.alpha = 1;
                    _facts.SetActive(true);

                    _updateZone = true;
                    _doodler.transform.position = _spawingSystem.GetLowestRespawnPoint() + Vector3.up * _respawnHeight;
                    _doodler.Activate();
                    _camera.SwitchState<CameraUpProgress>();
                });
            }
            else
            {
                OpenDefeatMenu();
            }
        }

        private void OpenDefeatMenu()
        {
            _defeatView.Show(_score.CurrentPoints, _dataLoader.CurrentGameData.BestResult);
            _defeatView.NextClicked += () =>
            {
                SceneManager.LoadScene(SceneManager.GetActiveScene().name);
            };
            _defeatView.ExitClicked += Exit;
        }

        public void Exit()
        {
            SceneManager.LoadScene(0);
        }

        /*private float CalculateScoreQuality(int score)
        {
            if (_dataLoader.CurrentGameData.Results.Count == 0)
                return 1;

            int resultsThatLessThanScore = 0;
            foreach (var result in _dataLoader.CurrentGameData.Results)
            {
                if (result < score)
                    resultsThatLessThanScore++;
            }

            return (float)resultsThatLessThanScore / (float)_dataLoader.CurrentGameData.Results.Count;
        }*/
    }
}
