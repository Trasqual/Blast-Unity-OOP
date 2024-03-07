using BlastGame.BoardItems.Core;
using BlastGame.BoardItems.Data;
using BlastGame.BoardManagement.Tiles;
using BlastGame.EventSystem;
using BlastGame.GridSystem;
using UnityEngine;

namespace BlastGame.BoardManagement
{
    public class CentralizedVerticalBoard : BoardBase
    {
        protected override void Awake()
        {
            base.Awake();

            _grid = Grid2D<Tile>.GetCenteralizedGrid(Center, _columns, _rows, CellSize);

            GenerateBoard();
            PopulateTiles();

            EventManager.Instance.TriggerEvent<BoardIsReadyEvent>(new BoardIsReadyEvent { Board = this });
        }

        protected override void GenerateBoard()
        {
            for (int i = 0; i < _columns; i++)
            {
                for (int j = 0; j < _rows; j++)
                {
                    Tile tile = new Tile(i, j, _grid.GetWorldPosition(i, j));

                    if (i > 0)
                    {
                        Tile leftTile = _grid.GetValueAt(i - 1, j);
                        tile.AddNeighbour(new Vector2Int(i - 1, j), leftTile);
                        leftTile.AddNeighbour(new Vector2Int(i + 1, j), tile);
                    }

                    if (j > 0)
                    {
                        Tile downTile = _grid.GetValueAt(i, j - 1);
                        tile.AddNeighbour(new Vector2Int(i, j - 1), downTile);
                        downTile.AddNeighbour(new Vector2Int(i, j + 1), tile);
                    }

                    _grid.SetValue(i, j, tile);
                }
            }
        }

        protected override void PopulateTiles()
        {
            for (int i = 0; i < _columns; i++)
            {
                for (int j = 0; j < _rows; j++)
                {
                    Tile tile = GetTile(i, j);

                    ItemData data = _levelData.GridData[i * _levelData.BoardData.Rows + j];
                    if (data != null)
                    {
                        BoardItem item = data.Build();
                        item.SetTileAndPosition(tile);
                        item.transform.SetParent(transform);
                    }

                }
            }
        }
    }
}
