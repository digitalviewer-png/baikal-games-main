using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace DoodleJump
{
    public class CameraControl : MonoBehaviour
    {
        [SerializeField] private Camera _camera;

        [Header("States")]
        [SerializeField] private CameraUpProgress _upProgressState;
        [SerializeField] private CameraVerticalFollow _verticalFollowState;

        private List<ICameraState> _states;
        private ICameraState _currentState;

        public Transform Target { get; private set; }

        public Vector3 CameraViewDownPoint => transform.position - Vector3.up * (_camera.orthographicSize);
        public float CameraViewportWidth =>  _camera.aspect * (_camera.orthographicSize);

        public void Init(Transform target)
        {
            Target = target;

            _states = new List<ICameraState>() {_upProgressState, _verticalFollowState, new CameraEmptyState()};
            SwitchState<CameraUpProgress>();
        }

        public void SwitchState<T>() where T : ICameraState
        {
            var newState = _states.First(st => st is T);

            if (newState == null)
                throw new ArgumentException($"Camera FSM doesn't have state {typeof(T).Name}");

            _currentState = newState;
        }

        private void Update()
        {
            _currentState.UpdateState(this);
        }
    }
}