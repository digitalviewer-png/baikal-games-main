using UnityEngine;
using UnityEngine.UI;

namespace BaikalGames.NerpasLabyrinth
{
    public class Obstacle : MonoBehaviour
    {
        void Start()
        {
            BoxCollider2D thisCollider = GetComponent<BoxCollider2D>();
            thisCollider.size = gameObject.GetComponent<Image>().rectTransform.sizeDelta;
        }
    }
}
