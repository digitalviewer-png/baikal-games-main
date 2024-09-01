using System;
using Lean.Touch;
using UnityEngine;

namespace PuzzleGame
{
    public class PuzzleDrawer : MonoBehaviour
    {
        [SerializeField] private LayerMask _layerMask;
        [SerializeField] private Palette _palette;
        [SerializeField] private DrawerTutorial _tutorial;

        public void Init()
        {
            _tutorial.StartTutorial();
        }

        private void OnEnable()
        {
            LeanTouch.OnFingerDown += OnPress;
        }

        private void OnDisable()
        {
            LeanTouch.OnFingerDown -= OnPress;
        }

        private void OnPress(LeanFinger finger)
        {
            var ray = Camera.main.ScreenPointToRay(finger.StartScreenPosition);
            RaycastHit2D[] raycastHits = new RaycastHit2D[1];

            if (Physics2D.Raycast(ray.origin, ray.direction, new ContactFilter2D() { layerMask = _layerMask }, raycastHits) > 0)
            {
                var drawingPiece = raycastHits[0].collider.GetComponent<DrawingPiece>();
                if (drawingPiece != null)
                {
                    drawingPiece.SetColor(_palette.SelectedDrawingColor);
                    _tutorial.EndTutorial();
                }
            }
        }
    }
}