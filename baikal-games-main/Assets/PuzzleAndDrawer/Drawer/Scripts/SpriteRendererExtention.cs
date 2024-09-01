using UnityEngine;

namespace PuzzleGame
{
    public static class SpriteRendererExtention
    {
        public static void SetColorAlpha(this SpriteRenderer renderer, float alpha)
        {
            var color = renderer.color;
            color.a = alpha;
            renderer.color = color;
        }
    }
}