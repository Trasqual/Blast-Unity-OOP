using UnityEngine;

namespace BlastGame.GridSystem
{
    public interface IGridPositionConverter
    {
        public Vector3 GridToWorldPosition(int x, int y);
        public Vector2Int WorldToGridPosition(Vector3 position);
    }
}
