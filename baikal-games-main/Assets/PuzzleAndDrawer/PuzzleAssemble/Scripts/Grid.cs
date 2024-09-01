using System;
using UnityEngine;

namespace PuzzleGame
{
    public class Grid
    {
        private int _width;
        private int _height;

        private float _cellSize;

        private Vector3 _leftDown;
        private Vector3 _rightUp;

        public int Width => _width;
        public int Height => _height;
        public float CellSize => _cellSize;
        public Vector3 LeftDown => _leftDown;
        public Vector3 RightUp => _rightUp;

        public Grid(float cellSize, Vector3 rightUp, Vector3 leftDown)
        {
            _rightUp = rightUp;
            _leftDown = leftDown;

            _cellSize = cellSize;

            _width = Mathf.RoundToInt((rightUp.x - leftDown.x) / cellSize);
            _height = Mathf.RoundToInt((rightUp.y - leftDown.y) / cellSize);
        }

        public Vector3 GetCellClosestCellPosition(Vector3 worldPos)
        {
            if (worldPos.x < _leftDown.x || worldPos.y < _leftDown.y ||
                worldPos.x > CellCenterWorldPos(_width, _height).x ||
                worldPos.y > CellCenterWorldPos(_width, _height).y)
                throw new ArgumentException("Position is out of grid");

            var cellX = Mathf.RoundToInt(worldPos.x / _cellSize);
            var cellY = Mathf.RoundToInt(worldPos.y / _cellSize);

            return CellCenterWorldPos(cellX, cellY);
        }

        private Vector2 CellCenterWorldPos(int cellX, int cellY) =>
            new Vector2((cellX - 0.5f) * _cellSize, (cellY - 0.5f) * _cellSize);
    }
}
