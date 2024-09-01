using Unity.VisualScripting;
using UnityEngine;

namespace BaikalGames.CleanBaikal
{
    public class TrashActivity : MonoBehaviour
    {
        public static float scorePerTrash;

        private GameObject trashType;
        public void TapOnObject()
        {
            tag = gameObject.tag;
            trashType = GameObject.FindGameObjectWithTag(tag);
            trashType.GetComponent<hslChanger>().ChangeSaturation();

            Score.count += scorePerTrash;
            Destroy(gameObject);
            Nerpa.instance.ShowMessage();
        }
    }
}
