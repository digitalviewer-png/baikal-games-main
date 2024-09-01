using UnityEngine;

namespace DoodleJump
{
    public class AnimatedJumpPad : JumpPad
    {
        [SerializeField] private Animator _animator;
        [SerializeField] private string _jumpTriggerName;

        protected override void OnJumped()
        {
            _animator.SetTrigger(_jumpTriggerName);
        }
    }
}