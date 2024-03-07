using BlastGame.EventSystem;
using BlastGame.MoveManagement;
using BlastGame.ServiceManagement;
using BlastGame.UI.Animations;
using System;
using TMPro;
using UnityEngine;

namespace BlastGame.UI.MoveSystem
{
    public class MoveUIItem : MonoBehaviour, IAnimatableUIItem
    {
        private event Action OnPlayAnimation;

        [SerializeField] private TMP_Text _text;

        private int _moves;

        private void Start()
        {
            Initialize(ServiceLocator.Instance.GetService<IMoveProvider>().GetMoves());
        }

        public void Initialize(int moves)
        {
            _moves = moves;

            SubscribeToEvents();

            UpdateText();
        }

        private void SubscribeToEvents()
        {
            EventManager.Instance.AddListener<MoveProcessedEvent>(OnMoveProcessed);
        }

        private void UpdateText()
        {
            _text.SetText(_moves.ToString());
        }

        public void OnMoveProcessed(object eventData)
        {
            int moveDelta = ((MoveProcessedEvent)eventData).ProcessedMoves;

            _moves += moveDelta;

            if (_moves < 0)
            {
                _moves = 0;
            }

            UpdateText();

            OnPlayAnimation?.Invoke();
        }

        public void AddListener(Action listener)
        {
            OnPlayAnimation += listener;
        }

        private void UnsubscribeToEvents()
        {
            EventManager.Instance.RemoveListener<MoveProcessedEvent>(OnMoveProcessed);
        }

        private void OnDisable()
        {
            UnsubscribeToEvents();
        }
    }
}
