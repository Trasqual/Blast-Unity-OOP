using BlastGame.BoardItems.Components;
using BlastGame.ServiceManagement;
using System.Collections.Generic;
using UnityEngine;

namespace BlastGame.FillsAndFallsSystem
{
    public class FallMovementController : MonoBehaviour, IService
    {
        private List<ItemFallHandler> _fallHandlers = new();

        public void RegisterToFallController(ItemFallHandler handler)
        {
            _fallHandlers.Add(handler);
        }

        public void Unregister(ItemFallHandler handler)
        {
            _fallHandlers.Remove(handler);
        }

        private void FixedUpdate()
        {
            for (int i = 0; i < _fallHandlers.Count; i++)
            {
                _fallHandlers[i].UpdateMovement();
            }
        }
    }
}
