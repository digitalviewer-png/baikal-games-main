using UnityEngine;

namespace BaikalGames.CleanBaikal
{
    public class WindowActivator : MonoBehaviour
    {
        [SerializeField] private GameObject additionalWindow;
        [SerializeField] private GameObject mainWindow;

        [SerializeField] private bool mainMenuBool;
        [SerializeField] private bool additionalWindowBool;

        public void OnClick()
        {
            mainWindow.SetActive(mainMenuBool);
            additionalWindow.SetActive(additionalWindowBool);

        }
    }
}
