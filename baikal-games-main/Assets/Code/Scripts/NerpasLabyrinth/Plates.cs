using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

namespace BaikalGames.NerpasLabyrinth
{
    public class Plates : MonoBehaviour
    {
        [Header("UI Components")]
        [SerializeField] private ChooseAction allActions;

        [Header("Plate Settings")]
        [SerializeField] private float platesRadius;

        [Header("Level Component")]
        [SerializeField] private Utility.InactivityTimer waitingNothing;

        private List<Plate> _plates = new();

        private bool _isCreatingLine = false;

        public List<Plate> plates { get => _plates; }
        public ChooseAction actions { get => allActions; }
        public float radius { get => platesRadius; }
        public bool isCreatingLine { get => _isCreatingLine; set => _isCreatingLine = value; }
        public Utility.InactivityTimer waiting { get => waitingNothing; }

        public void Win()
        {
            List<Vector2> winPoints = new List<Vector2>();

            Canvas canvas = actions.canvas;
            Vector2 scale = new(
                canvas.pixelRect.width / Screen.width,
                canvas.pixelRect.height / Screen.height
                );

            foreach (Plate plate in _plates)
            {
                Vector2 thisPoint = ((RectTransform)plate.transform).anchoredPosition;
                thisPoint = new Vector2(thisPoint.x * scale.x, thisPoint.y * scale.y);

                winPoints.Add(thisPoint);
            }
            _isCreatingLine = false;

            StartCoroutine(actions.WinAnimation(winPoints));
        }

        public void LinesOver()
        {
            if (plates.Count > 0) plates[_plates.Count - 1].LastPlate();
            //_isCreatingLine = true;

            //if (_plates.Count > 0) _plates[_plates.Count - 1].StartAnimation();
            //_isCreatingLine = false;

            // _plates.Clear();

            isCreatingLine = false;
        }
    }
}
