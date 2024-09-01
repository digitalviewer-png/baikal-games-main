using UnityEngine;
using UnityEngine.U2D;

namespace PuzzleGame
{
    public class BrushView : MonoBehaviour
    {
        [SerializeField] private SpriteRenderer _brush;

        public void SetColor(DrawingColor color)
        {
            _brush.color = color.DefaultColor;
        }
    }
}