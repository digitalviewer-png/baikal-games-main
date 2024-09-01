using System.Collections;
using DG.Tweening;
using UnityEngine;

namespace DoodleJump
{
    public class Flight : Bonus
    {
        [SerializeField] private Animator _animator;
        [SerializeField] private string _flightEndTriggerName;
        [SerializeField] private Vector3 _inDoodlerPosition = Vector3.zero;

        [Space]
        [SerializeField] private float _destractionDelay = 1f;
        [SerializeField] private float _flightHeight;
        [SerializeField] private float _flightTime;
        
        public override void Init(Doodler doodler)
        {
            StartCoroutine(FlightRoutine(doodler));
        }

        private void OnDestroy()
        {
            StopAllCoroutines();
        }

        private IEnumerator FlightRoutine(Doodler doodler)
        {
            doodler.EnableColliders(false);
            doodler.Animator.SetBool("IsFlying", true);

            var standartGravity = doodler.Rigidbody.gravityScale;
            doodler.Rigidbody.gravityScale = 0;

            var flightVelocity = _flightHeight / _flightTime;
            doodler.Rigidbody.velocity = new Vector2(doodler.Rigidbody.velocity.x, flightVelocity);

            transform.parent = doodler.transform;
            transform.localPosition = _inDoodlerPosition;
            transform.localScale = Vector3.one;

            yield return new WaitForSeconds(_flightTime);

            doodler.EnableColliders(true);
            doodler.Rigidbody.gravityScale = standartGravity;

            _animator.SetTrigger(_flightEndTriggerName);

            doodler.Animator.SetBool("IsFlying", false);
            Destroy(gameObject, _destractionDelay);
            
        }
    }
}
