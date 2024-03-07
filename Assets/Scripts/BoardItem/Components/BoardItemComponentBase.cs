using BlastGame.BoardItems.Core;
using UnityEngine;

namespace BlastGame.BoardItems.Components
{
    [RequireComponent(typeof(BoardItem))]
    public abstract class BoardItemComponentBase : MonoBehaviour
    {
        protected BoardItem _attachedItem;

        protected virtual void Awake()
        {
            _attachedItem = GetComponent<BoardItem>();
        }
    }
}
