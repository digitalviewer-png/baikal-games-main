using Lean.Touch;
using UnityEngine;

namespace PuzzleGame
{
    public class Palette : MonoBehaviour
    {
        [SerializeField] private LayerMask _layerMask;
        [SerializeField] private DrawingColor _startDrawingColor;
        [SerializeField] private BrushView _brush;

        public DrawingColor SelectedDrawingColor { get; private set; }

        private void Awake()
        {
            SelectedDrawingColor = _startDrawingColor;
            _brush.SetColor(SelectedDrawingColor);
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
                var drawingPiece = raycastHits[0].collider.GetComponent<PaletteButton>();
                if (drawingPiece != null)
                {
                    SelectedDrawingColor = drawingPiece.DrawingColor;
                    _brush.SetColor(SelectedDrawingColor);
                }
            }
        }
    }
}