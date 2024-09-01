using System;
using UnityEngine;

namespace DoodleJump
{
    [Serializable]
    public class CameraUpProgress : ICameraState
    {
        [SerializeField] private float _offset = 0f;

        public void UpdateState(CameraControl control)
        {
            var targetY = control.Target.position.y;
            var cameraY = control.transform.position.y;

            if (targetY > cameraY - _offset)
            {
                var position = control.transform.position;
                position.y = targetY + _offset;
                control.transform.position = position;
            }
        }
    }
}
