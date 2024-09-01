using UnityEngine;

namespace BaikalGames.CleanBaikal
{
    public class ReturnSaturation : MonoBehaviour
    {
        [SerializeField] private GameObject[] trashUI;

        public void ChangeSaturationToDefault()
        {
            for(int  i = 0; i < trashUI.Length; i++)
            {
                trashUI[i].GetComponent<hslChanger>().DefaultSaturation();
            }
        }

        private void OnApplicationQuit()
        {
            ChangeSaturationToDefault();
        }
    }
}
