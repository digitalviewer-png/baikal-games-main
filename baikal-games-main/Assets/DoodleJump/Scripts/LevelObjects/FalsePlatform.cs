using UnityEngine;

namespace DoodleJump
{
    public class FalsePlatform : TypedTrigger<Doodler>
    {
        [SerializeField] private Animator _animator;
        [SerializeField] private string _destroyTrigger;
        [SerializeField] private Collider2D _collider;

        private void OnEnable()
        {
            TriggerEnter += CollisionWithDoodler;
        }

        private void OnDisable()
        {
            TriggerExit -= CollisionWithDoodler;
        }

        private void CollisionWithDoodler(Doodler doodler)
        {
            if (doodler.Rigidbody.velocity.y < 0)
            {
                _collider.enabled = false;
                _animator.SetTrigger(_destroyTrigger);
            }
        }
    }
}