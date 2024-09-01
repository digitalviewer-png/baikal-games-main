using System;
using UnityEngine;

namespace DoodleJump
{
    public class Doodler : MonoBehaviour, IZoneObject
    {
        [SerializeField] private Rigidbody2D _rigidbody;
        [SerializeField] private Collider2D _jumpCollider;
        [SerializeField] private float _horizontalMove;
        [SerializeField] private Animator _animator;

        private IPlayerInput _input;
        private Zone _zone;
        private bool _isActive;

        public event Action ZoneLeft;

        public Rigidbody2D Rigidbody => _rigidbody;
        public Animator Animator => _animator;

        public void Init(IPlayerInput input)
        {
            _isActive = true;
            _input = input;
        }

        public void SetZone(Zone zone)
        {
            _zone = zone;
        }

        public void Activate()
        {
            EnableColliders(true);
            _isActive = true;
        }
        public void Deactivate()
        {
            EnableColliders(false);
            _isActive = false;
        }

        public void Jump(float jumpVelocity)
        {
            _animator.SetTrigger("Jump");
            var velocity = Rigidbody.velocity;
            velocity.y = jumpVelocity;
            Rigidbody.velocity = velocity;
        }

        public void EnableColliders(bool isEnabled)
        {
            _jumpCollider.enabled = isEnabled;
        }

        private void FixedUpdate()
        {
            if (_isActive)
            {
                var inputValue = _input.GetInput();

                var velocity = Rigidbody.velocity;
                velocity.x = inputValue * _horizontalMove;
                Rigidbody.velocity = velocity;

                if (inputValue != 0)
                {
                    var scale = transform.localScale;
                    scale.x = _input.GetInput() < 0 ? -1 : 1;
                    transform.localScale = scale;
                }

                if (_zone.IsOutOfZoneVertically(Rigidbody.transform.position.y))
                    ZoneLeft?.Invoke();
            }

            var position = Rigidbody.position;
            position.x = _zone.LoopInsideZoneHorizontally(position.x);
            Rigidbody.position = position;
        }
    }
}
