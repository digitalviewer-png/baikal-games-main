using System;
using Lean.Touch;
using UnityEngine;

namespace PuzzleGame
{
    public class LeanTouchPressAndDragInput : MonoBehaviour, IPressAndDragInput
    {
        public event Action<Vector2> OnPress;
        public event Action<Vector2> OnDrag;
        public event Action<Vector2> OnUnpress;

        private LeanFinger _currentFinger;

        private void OnEnable()
        {
            LeanTouch.OnFingerDown += OnFingerDown;
            LeanTouch.OnFingerUp += OnFingerUp;
        }

        private void OnDisable()
        {
            LeanTouch.OnFingerDown -= OnFingerDown;
            LeanTouch.OnFingerUp -= OnFingerUp;
        }

        private void Update()
        {
            if (_currentFinger != null)
            {
                OnDrag?.Invoke(_currentFinger.LastScreenPosition);
            }
        }

        private void OnFingerDown(LeanFinger finger)
        {
            if (_currentFinger == null)
            {
                _currentFinger = finger;
                OnPress?.Invoke(_currentFinger.StartScreenPosition);
            }
        }

        private void OnFingerUp(LeanFinger finger)
        {
            if (_currentFinger == finger)
            {
                OnUnpress?.Invoke(_currentFinger.LastScreenPosition);
                _currentFinger = null;
            }
        }

    }
}