using BlastGame.BoardItems.Components;
using BlastGame.BoardItems.Core;
using BlastGame.BoardItems.Data;
using BlastGame.StateMachineSystem;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

namespace BlastGame.BoardManagement.Tiles
{
    public class Tile
    {
        public event Action<object, ItemData> OnPop;
        public event Action<object, ItemData> OnMerge;

        public int X { get; private set; }
        public int Y { get; private set; }
        public Vector3 Position { get; private set; }

        private StateMachine _stateMachine;

        public IState State => _stateMachine.GetState();

        private BoardItem _currentItem;

        public Dictionary<Vector2Int, Tile> Neighbours { get; private set; } = new();

        #region Neighbours
        public Tile Up
        {
            get
            {
                Vector2Int dir = new Vector2Int(X, Y + 1);
                if (Neighbours.ContainsKey(dir))
                {
                    return Neighbours[dir];
                }
                return null;
            }
        }

        public Tile Down
        {
            get
            {
                Vector2Int dir = new Vector2Int(X, Y - 1);
                if (Neighbours.ContainsKey(dir))
                {
                    return Neighbours[dir];
                }
                return null;
            }
        }

        public Tile Right
        {
            get
            {
                Vector2Int dir = new Vector2Int(X + 1, Y);
                if (Neighbours.ContainsKey(dir))
                {
                    return Neighbours[dir];
                }
                return null;
            }
        }

        public Tile Left
        {
            get
            {
                Vector2Int dir = new Vector2Int(X - 1, Y);
                if (Neighbours.ContainsKey(dir))
                {
                    return Neighbours[dir];
                }
                return null;
            }
        }
        #endregion

        public Tile(int x, int y, Vector3 position)
        {
            X = x;
            Y = y;
            Position = position;

            _currentItem = null;

            _stateMachine = new StateMachine();
        }

        public void AddNeighbour(Vector2Int direction, Tile neighbour)
        {
            Neighbours.Add(direction, neighbour);
        }

        public void SetItem(BoardItem element)
        {
            _currentItem = element;
        }

        public BoardItem GetItem()
        {
            return _currentItem;
        }

        public void Pop(object poppedBy)
        {
            if (_currentItem == null)
            {
                return;
            }

            if (_currentItem.TryGetComponent(out ItemPopHandler handler))
            {
                if (!handler.CanBePoppedBy(poppedBy))
                {
                    ChangeState(new TileSettledState());
                    return;
                }

                OnPop?.Invoke(poppedBy, _currentItem.GetData());

                handler.Pop();
            }
        }

        public async Task Merge(object mergedBy, Vector3 mergeCenter)
        {
            if (_currentItem.TryGetComponent(out ItemMergeHandler mergeHandler))
            {
                OnMerge?.Invoke(mergedBy, _currentItem.GetData());

                await mergeHandler.Merge(mergeCenter);

                SetItem(null);
                ChangeState(new TileEmptyState());
            }
        }

        public void ChangeState(IState state)
        {
            _stateMachine.SetState(state);
        }
    }
}