using System;
using Lean.Touch;
using UnityEngine;

namespace DoodleJump
{
    public class LeanTouchInput : IPlayerInput, IDisposable
    {
        private LeanFinger _currentFinger;
        private readonly float _inputZoneSize;

        public LeanTouchInput(float inputZoneSize)
        {
            _inputZoneSize = inputZoneSize;

            LeanTouch.OnFingerDown += OnFingerDown;
            LeanTouch.OnFingerUp += OnFingerUp;
        }

        private void OnFingerDown(LeanFinger finger)
        {
            if (_currentFinger == null)
                _currentFinger = finger;
        }

        private void OnFingerUp(LeanFinger finger)
        {
            if (_currentFinger == finger)
                _currentFinger = null;
        }

        public float GetInput()
        {
            if (_currentFinger != null)
            {
                var screenDistance = _currentFinger.ScreenPosition.x - _currentFinger.StartScreenPosition.x;
                return Mathf.Clamp(screenDistance / _inputZoneSize, -1, 1);
            }

            return 0f;
        }

        public void Dispose()
        {
            LeanTouch.OnFingerDown -= OnFingerDown;
            LeanTouch.OnFingerDown -= OnFingerUp;
        }
    }
}