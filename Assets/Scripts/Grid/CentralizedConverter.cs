using UnityEngine;

namespace BlastGame.GridSystem
{
    public class CentralizedConverter : IGridPositionConverter
    {
        private Vector3 _center;
        private int _columns;
        private int _rows;
        private float _cellSize;

        public CentralizedConverter(Vector3 center, int columns, int rows, float cellSize)
        {
            _center = center;
            _columns = columns;
            _rows = rows;
            _cellSize = cellSize;
        }

        public Vector3 GridToWorldPosition(int x, int y)
        {
            float posX = -_columns * _cellSize * 0.5f + _cellSize * 0.5f + x * _cellSize + _center.x;
            float posY = -_rows * _cellSize * 0.5f + _cellSize * 0.5f + y * _cellSize + _center.y;

            return new Vector3(posX, posY, _center.z);
        }

        public Vector2Int WorldToGridPosition(Vector3 position)
        {
            Vector3 normalizedPosition = (position - _center + new Vector3(_cellSize * _columns * 0.5f, _cellSize * _rows * 0.5f, 0f)) / _cellSize;
            int posX = Mathf.FloorToInt(normalizedPosition.x);
            int posY = Mathf.FloorToInt(normalizedPosition.y);

            return new Vector2Int(posX, posY);
        }
    }
}
