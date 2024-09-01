using Lean.Touch;
using UnityEngine;

namespace PuzzleGame
{
    public class MenuChooser : MonoBehaviour
    {
        [SerializeField] private LayerMask _layerMask;
        [SerializeField] private PuzzleAssembler _assembler;

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
                var pictureChooseButton = raycastHits[0].collider.GetComponent<PictureChooseButton>();
                if (pictureChooseButton != null)
                {
                    _assembler.StartPuzzle(pictureChooseButton.PuzzleData);
                }
            }
        }
    }
}