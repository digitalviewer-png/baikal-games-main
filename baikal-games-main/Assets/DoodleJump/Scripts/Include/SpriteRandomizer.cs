using System.Collections.Generic;
using UnityEngine;

namespace DoodleJump
{
    [RequireComponent(typeof(SpriteRenderer))]
    public class SpriteRandomizer : MonoBehaviour
    {
        [SerializeField] private List<Sprite> _possibleSprites = new List<Sprite>();

        private void Start()
        {
            GetComponent<SpriteRenderer>().sprite = _possibleSprites[Random.Range(0, _possibleSprites.Count)];
        }
    }
}