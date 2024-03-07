using System;
using UnityEngine;

namespace BlastGame.BoardManagement
{
    [Serializable]
    public struct BoardData
    {
        public Vector3 Center;

        [Space(10)]
        [Range(2, 10)]
        public int Columns;

        [Range(2, 10)]
        public int Rows;

        [Space(10)]
        public float CellSize;
    }
}
