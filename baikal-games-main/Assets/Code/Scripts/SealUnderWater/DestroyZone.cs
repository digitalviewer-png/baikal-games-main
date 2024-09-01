using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BaikalGames
{
    public class DestroyZone : MonoBehaviour
    {
        private void OnTriggerEnter(Collider other)
        {
            if (other.transform.CompareTag("Player")) return;
            if (other.transform.CompareTag("Obstacle"))
            {
                Destroy(other.transform.parent.gameObject);
                return;
            }
            Destroy(other.gameObject);
        }
    }
}
