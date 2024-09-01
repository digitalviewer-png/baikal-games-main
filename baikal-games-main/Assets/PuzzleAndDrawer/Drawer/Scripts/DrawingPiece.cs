using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

namespace PuzzleGame
{
    public class DrawingPiece : MonoBehaviour
    {
        [Serializable]
        private class ColorVariant
        {
            [SerializeField] private DrawingColor _drawingColor;
            [SerializeField] private Sprite _sprite;

            public DrawingColor DrawingColor => _drawingColor;

            public void SetSprite(SpriteRenderer renderer)
            {
                renderer.SetColorAlpha(1f);
                renderer.sprite = _sprite;
            }
        }

        [SerializeField] private List<ColorVariant> _variants = new List<ColorVariant>();
        [SerializeField] private SpriteRenderer _renderer;

        public void ClearColor()
        {
            _renderer.SetColorAlpha(0f); 
        }

        public void SetColor(DrawingColor drawingColor)
        {
            var variant = _variants.Find(x => x.DrawingColor == drawingColor);

            if (variant == null)
                throw new ArgumentException($"This piece doesnt have variant for {drawingColor}");

            variant.SetSprite(_renderer);
        }
    }
}
