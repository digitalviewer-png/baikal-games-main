using UnityEngine;

namespace BaikalGames
{
    public class TimeScaleRecover : MonoBehaviour
    {
        private void Start()
        {
            Time.timeScale = 1f;
            Time.fixedDeltaTime = 0.02f;
        }
    }
}
