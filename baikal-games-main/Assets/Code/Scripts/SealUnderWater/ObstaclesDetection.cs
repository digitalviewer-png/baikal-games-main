using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;
namespace BaikalGames.UnderwaterSealAdvancher
{
    public class ObstaclesDetection : MonoBehaviour
    {
        [SerializeField] private PlayerScore score;
        [SerializeField] private UnityEvent events = new UnityEvent();

        private void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag("Fish"))
            {
                if (score == null) return;
                Fish fish = other.transform.GetComponent<Fish>();
                if (fish == null) return;
                score.CollectFish(fish);
                Destroy(other.gameObject);
            }
            if (!other.CompareTag("Obstacle")) return;
            Iceberg iceberg = other.transform.parent.GetComponent<Iceberg>();
            iceberg.Destroy();
            events.Invoke();
            
        }
    }
}
