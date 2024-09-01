using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace BaikalGames
{
    public class FreezeUI : MonoBehaviour
    {

        [SerializeField] GraphicRaycaster graphicRaycaster;
        [SerializeField] float secondsToFreeze;

        private void Awake()
        {
            if (Application.isEditor) return;
            graphicRaycaster.enabled = false;
        }

        private void Start()
        {
            if (Application.isEditor) return;
            StartCoroutine(FreezingUI());
        }

        private IEnumerator FreezingUI()
        {
            yield return new WaitForSeconds(secondsToFreeze);
            graphicRaycaster.enabled = true;
            yield return null;
        }

    }
}
