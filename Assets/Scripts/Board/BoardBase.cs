using BlastGame.BoardItems.Factory;
using BlastGame.BoardManagement.Tiles;
using BlastGame.GridSystem;
using BlastGame.LevelManagement;
using BlastGame.LevelManagement.Datas;
using BlastGame.ServiceManagement;
using UnityEngine;

namespace BlastGame.BoardManagement
{
    public abstract class BoardBase : MonoBehaviour, IService
    {
        protected LevelData _levelData;

        protected Grid2D<Tile> _grid;

        protected BoardItemFactory _itemFactory;

        public Vector2Int Size => new Vector2Int(_columns, _rows);
        public int ItemCount => _columns * _rows;
        public float CellSize { get; private set; }
        public Vector3 Center { get; private set; }

        protected int _columns;
        protected int _rows;

        protected virtual void Awake()
        {
            Initialize();

            Center = _levelData.BoardData.Center;
            _columns = _levelData.BoardData.Columns;
            _rows = _levelData.BoardData.Rows;
            CellSize = _levelData.BoardData.CellSize;
        }

        public void Initialize()
        {
            _itemFactory = ServiceLocator.Instance.GetService<BoardItemFactory>();
            _levelData = ServiceLocator.Instance.GetService<ILevelManager>().GetLevelData();
        }

        protected abstract void GenerateBoard();
        protected abstract void PopulateTiles();

        public Tile GetTile(int x, int y)
        {
            return _grid.GetValueAt(x, y);
        }

        public Tile GetTileFromPosition(Vector3 position)
        {
            return _grid.GetValueFromPosition(position);
        }

        public Vector3 GetAlignedPosition(int x, int y)
        {
            return _grid.GetGridAlignedWorldPosition(x, y);
        }

        public Vector2Int GetGridFromWorldPosition(Vector3 position)
        {
            return _grid.GetGridFromWorldPosition(position);
        }
    }
}
