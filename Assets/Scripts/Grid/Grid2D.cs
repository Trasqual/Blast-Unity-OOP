using UnityEngine;

namespace BlastGame.GridSystem
{
    public class Grid2D<T>
    {
        private readonly Vector3 _center;
        private readonly int _columns;
        private readonly int _rows;
        private readonly float _cellSize;
        private readonly T[,] _values;

        private IGridPositionConverter _converter;

        public Grid2D(Vector3 center, int columns, int rows, float cellSize, IGridPositionConverter converter)
        {
            _center = center;
            _columns = columns;
            _rows = rows;
            _cellSize = cellSize;
            _converter = converter ?? new CentralizedConverter(_center, columns, rows, _cellSize);
            _values = new T[columns, rows];
        }

        public static Grid2D<T> GetCenteralizedGrid(Vector3 center, int columns, int rows, float cellSize = 1)
        {
            return new Grid2D<T>(center, columns, rows, cellSize, new CentralizedConverter(center, columns, rows, cellSize));
        }

        public void SetValue(int x, int y, T value)
        {
            if (!IsValid(x, y))
            {
                Debug.Log($"Failed to set grid value. ({x},{y}) is not a valid grid position!");
                return;
            }
            _values[x, y] = value;
        }

        public T GetValueAt(int x, int y)
        {
            return IsValid(x, y) ? _values[x, y] : default;
        }

        public T GetValueAt(Vector2Int index)
        {
            return GetValueAt(index.x, index.y);
        }

        public T GetValueFromPosition(Vector3 position)
        {
            var gridPos = _converter.WorldToGridPosition(position);
            if (IsValid(gridPos.x, gridPos.y))
            {
                return _values[gridPos.x, gridPos.y];
            }

            return default;
        }

        public bool IsValid(int x, int y)
        {
            return x > -1 && x < _columns && y > -1 && y < _rows;
        }

        public Vector3 GetWorldPosition(int x, int y)
        {
            if (!IsValid(x, y))
            {
                throw new System.Exception($"Failed to get world position. ({x},{y}) is not a valid grid position!");
            }
            return _converter.GridToWorldPosition(x, y);
        }

        public Vector3 GetGridAlignedWorldPosition(int x, int y)
        {
            return _converter.GridToWorldPosition(x, y);
        }

        public Vector2Int GetGridFromWorldPosition(Vector3 position)
        {
            return _converter.WorldToGridPosition(position);
        }
    }
}
