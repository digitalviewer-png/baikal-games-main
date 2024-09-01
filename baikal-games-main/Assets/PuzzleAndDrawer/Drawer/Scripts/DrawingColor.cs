using UnityEngine;

namespace PuzzleGame
{
    [CreateAssetMenu(menuName = "DATA/DrawingColor", fileName = "DrawingColor")]
    public class DrawingColor : ScriptableObject
    {
        [SerializeField] private Color _defaultColor;

        public Color DefaultColor => _defaultColor;
    }
}