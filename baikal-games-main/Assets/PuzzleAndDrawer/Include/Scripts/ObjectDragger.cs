using System;
using UnityEngine;

namespace PuzzleGame
{
    public class ObjectDragger<T> : IDisposable where T : Component
    {
        private Camera _camera;
        private LayerMask _layerMask;
        private IPressAndDragInput _input;

        private T _currentlyDraggedObject;
        private float _zPosition;

        public Action<T> ObjectDragBegan;
        public Action<T> ObjectDragEnd;

        public ObjectDragger(Camera camera, LayerMask layerMask, IPressAndDragInput input)
        {
            _camera = camera;
            _layerMask = layerMask;
            _input = input;

            _input.OnPress += Press;
            _input.OnUnpress += Unpress;
        }

        private void Press(Vector2 pressPosition)
        {
            var ray = _camera.ScreenPointToRay(pressPosition);
            RaycastHit2D[] raycastHits = new RaycastHit2D[1];

            if (Physics2D.Raycast(ray.origin, ray.direction, new ContactFilter2D() { layerMask = _layerMask },
                    raycastHits) > 0)
            {
                _currentlyDraggedObject = raycastHits[0].collider.GetComponent<T>();
                if (_currentlyDraggedObject != null)
                {
                    _zPosition = _currentlyDraggedObject.transform.position.z;
                    ObjectDragBegan?.Invoke(_currentlyDraggedObject);
                    _input.OnDrag += OnDrag;
                }
            }
        }

        private void Unpress(Vector2 unpressPosition)
        {
            if (_currentlyDraggedObject != null)
            {
                ObjectDragEnd?.Invoke(_currentlyDraggedObject);
                _input.OnDrag -= OnDrag;
                _currentlyDraggedObject = null;
            }
        }

        private void OnDrag(Vector2 dragPosition)
        {
            var worldPoint = _camera.ScreenToWorldPoint(dragPosition);
            worldPoint.z = _zPosition;
            _currentlyDraggedObject.transform.position = worldPoint;
        }

        public void Dispose()
        {
            _input.OnPress -= Press;
            _input.OnUnpress -= Unpress;
            _input.OnDrag -= OnDrag;
        }
    }
}