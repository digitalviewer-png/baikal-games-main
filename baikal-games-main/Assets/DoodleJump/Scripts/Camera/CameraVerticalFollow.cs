using System;
using UnityEngine;

namespace DoodleJump
{
    [Serializable]
    public class CameraVerticalFollow : ICameraState
    {
        [SerializeField] private float _yOffset = 5f;
        [SerializeField] private float _followSpeed = 5f;

        public void UpdateState(CameraControl control)
        {
            var targetY = control.Target.position.y;
            var position = control.transform.position;
            position.y = Mathf.Lerp(position.y, targetY + _yOffset, Time.deltaTime * _followSpeed);
            control.transform.position = position;
        }
    }
}