using UnityEngine;

namespace DoodleJump
{
    [SelectionBase]
    public class JumpPad : TypedCollisionHandler<Doodler>
    {
        [SerializeField] protected float _jumpForce = 10f;

        private void OnEnable()
        {
            CollisionEnter += CollisionWithDoodler;
            CollisionStay += CollisionStayWithDoodler;
        }

        private void OnDisable()
        {
            CollisionEnter -= CollisionWithDoodler;
            CollisionStay -= CollisionStayWithDoodler;
        }

        private void CollisionWithDoodler(Doodler doodler, Collision2D collision)
        {
            if (collision.relativeVelocity.y < 0)
            {
                doodler.Jump(_jumpForce);
                OnJumped();
            }
        }

        private void CollisionStayWithDoodler(Doodler doodler, Collision2D collision)
        {
            if (doodler.Rigidbody.velocity.y <= 0)
            {
                doodler.Jump(_jumpForce);
                OnJumped();
            }
        }

        protected virtual void OnJumped()
        { }
    }
}