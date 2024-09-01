using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace BaikalGames.NerpasLabyrinth
{
    public class ChooseAction : MonoBehaviour
    {
        [Header("UI Objects")]
        [SerializeField] private Canvas currentCanvas;

        [SerializeField] private GameObject pauseScreen;

        [SerializeField] private GameObject winScreen;
        [SerializeField] private Color winColor = Color.white;

        [SerializeField] private GameObject looseScreen;

        [SerializeField] private GameObject player;

        public Canvas canvas { get => currentCanvas; }

        public void Pause(bool isActive)
        {
            pauseScreen.SetActive(isActive);
        }

        public void Win(bool isActive)
        {
            winScreen.SetActive(isActive);
        }

        public IEnumerator WinAnimation(List<Vector2> points)
        {
            player.transform.SetParent(currentCanvas.transform, true);

            player.GetComponent<Image>().color = winColor;

            for (int i = 1; i < points.Count; i++)
            {
                Vector2 startPosition = points[i - 1];
                Vector2 targetPosition = points[i];

                float elapsedTime = 0;
                float animationDuration = 0.03f;

                while (elapsedTime < animationDuration)
                {
                    float t = elapsedTime / animationDuration;
                    ((RectTransform)player.transform).anchoredPosition = Vector2.Lerp(startPosition, targetPosition, t);
                    yield return null;
                    elapsedTime += Time.deltaTime;
                }

                ((RectTransform)player.transform).anchoredPosition = targetPosition;
            }

            Win(true);
            player.SetActive(false);
        }
    }
}
