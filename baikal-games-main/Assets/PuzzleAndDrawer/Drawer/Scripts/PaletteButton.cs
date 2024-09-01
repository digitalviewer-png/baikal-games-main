using UnityEngine;

namespace PuzzleGame
{
    [RequireComponent(typeof(SpriteRenderer))]
    public class PaletteButton : MonoBehaviour
    {
        [SerializeField] private DrawingColor _drawingColor;

        public DrawingColor DrawingColor => _drawingColor;

        private void Awake()
        {
            GetComponent<SpriteRenderer>().color = _drawingColor.DefaultColor;
        }
    }
}