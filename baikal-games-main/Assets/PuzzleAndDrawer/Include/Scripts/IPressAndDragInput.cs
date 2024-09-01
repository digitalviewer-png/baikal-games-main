using System;
using UnityEngine;

namespace PuzzleGame
{
    public interface IPressAndDragInput
    {
        public event Action<Vector2> OnPress;
        public event Action<Vector2> OnDrag;
        public event Action<Vector2> OnUnpress;
    }
}