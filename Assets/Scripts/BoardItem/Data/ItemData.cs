using BlastGame.BoardItems.Core;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace BlastGame.BoardItems.Data
{
    [Serializable]
    public abstract class ItemData : ScriptableObject
    {
        [field: SerializeField] public List<Sprite> Sprites { get; private set; } = new List<Sprite>();
        public ItemColor Color;

        public Sprite GetInitialSprite() => Sprites[0];

        public abstract BoardItem Build();
    }
}
