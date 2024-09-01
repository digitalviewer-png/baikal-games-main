using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

namespace PuzzleGame
{
    public class PuzzleAssembler : MonoBehaviour
    {
        [Space]
        [SerializeField] private TransitionAnimation _startScene;

        [Space]
        [SerializeField] private Transform _outPoint;
        [SerializeField] private Transform _fromPoint;
        [SerializeField] private Transform _centerPoint;

        [Space]
        [SerializeField] private Button _closeButton;
        [SerializeField] private Button _backButton;

        private PuzzleData _currentData;
        private Puzzle _currentPuzzle;
        private Puzzle _currentDrawer;
        private TransitionAnimation _currentScene;
        private HashSet<AssemblingPiece> _assemblingPieces = new HashSet<AssemblingPiece>();

        private bool _isPuzzleState;

        private void Start()
        {
            _startScene.UpdatePositions();
        }

        private void OnEnable()
        {
            _backButton.onClick.AddListener(Back);
        }

        private void OnDisable()
        {
            _backButton.onClick.RemoveListener(Back);
        }

        public void StartPuzzle(PuzzleData data)
        {
            _currentData = data;
            _isPuzzleState = true;
            _currentPuzzle = data.GetPuzzleInstance((_fromPoint));

            _currentPuzzle.PuzzleTransitionAnimation.UpdatePositions();
            
            _assemblingPieces.Clear();

            foreach (var piece in _currentPuzzle.AssemblingPieces)
            {
                piece.PlacedRight += OnPiecePlacedRight;
            }

            StopAllCoroutines();
            StartCoroutine(MoveToDrawerRoutine(_startScene, _currentPuzzle.PuzzleTransitionAnimation, 0f, () =>
            {
                _closeButton.gameObject.SetActive(false);
                _backButton.gameObject.SetActive(true);
                _currentPuzzle.StartPuzzle();
            }));
        }

        private void OnPiecePlacedRight(AssemblingPiece piece)
        {
            _assemblingPieces.Add(piece);

            if (_isPuzzleState && _assemblingPieces.Count == _currentPuzzle.AssemblingPieces.Length)
            {
                _isPuzzleState = false;
                _currentPuzzle.SetFullState();
                _currentPuzzle.WinAnimation.Play();

                _currentDrawer = _currentData.GetPuzzleInstance(_fromPoint);
                _currentDrawer.DrawerTransitionAnimation.UpdatePositions();

                foreach (var p in _currentPuzzle.AssemblingPieces)
                {
                    p.PlacedRight -= OnPiecePlacedRight;
                }

                StopAllCoroutines();
                StartCoroutine(MoveToDrawerRoutine(_currentPuzzle.PuzzleTransitionAnimation, _currentDrawer.DrawerTransitionAnimation, 2f, () =>
                {
                    _currentDrawer.StartDrawer();
                }));
            }
        }

        private void Back()
        {
            StopAllCoroutines();
            StartCoroutine(MoveToDrawerRoutine(_currentScene, _startScene, 0f, () =>
            {
                _closeButton.gameObject.SetActive(true);
                _backButton.gameObject.SetActive(false);

                Destroy(_currentPuzzle.gameObject);

                if (_currentDrawer != null)
                    Destroy(_currentDrawer.gameObject);
            }));
        }

        private IEnumerator MoveToDrawerRoutine(TransitionAnimation hideScene, TransitionAnimation showScene, float time, Action onComplete)
        {
            yield return new WaitForSeconds(time);

            _currentScene = showScene;
            showScene.transform.parent.gameObject.SetActive(true);
            showScene.SetInPosition(_fromPoint.position);

            hideScene.Move(_outPoint, 1f);
            showScene.Move(_centerPoint, 1f).SetDelay(0.2f);

            yield return new WaitForSeconds(0.65f);
            hideScene.transform.parent.gameObject.SetActive(false);
            onComplete();
        }
    }
}