using UnityEngine;
using UnityEngine.Events;

namespace BaikalGames.UnderwaterSealAdvancher
{
    public class Iceberg : MonoBehaviour
    {
        [SerializeField] private Animator animator;
        [SerializeField] private UnityEvent action;

        public void Destroy()
        {
            animator.SetTrigger("Destroy");
            action.Invoke();
        }
        public void DestroyParent()
        {
            Destroy(transform.parent.gameObject);
        }
    }
}
