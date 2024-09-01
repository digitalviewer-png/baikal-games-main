using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
namespace BaikalGames.UnderwaterSealAdvancher
{
    public class DirectionalMovement : MonoBehaviour
    {
        public float speed;
        [SerializeField] private Vector3 direction;
        [SerializeField] private float startSpeed, addSpeedPerSecond;

        private IEnumerator Start()
        {
            speed = startSpeed;
            while (true)
            {
                speed += addSpeedPerSecond * Time.deltaTime;
                yield return null;
            }
        }
        private void Update()
        {
            transform.position += direction * Time.deltaTime * speed;
        }
        public void Stop()
        {
            speed = 3f;
        }
    }
}