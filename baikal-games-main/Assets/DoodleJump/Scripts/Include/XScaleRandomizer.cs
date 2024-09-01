using UnityEngine;

namespace DoodleJump
{
    public class XScaleRandomizer : MonoBehaviour
    {
        private void Start()
        {
            var scale = transform.localScale;
            scale.x = Random.Range(0f, 1f) > 0.5f ? -1 : 1;
            transform.localScale = scale;
        }
    }
}
