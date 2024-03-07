using BlastGame.LevelManagement.Datas;
using BlastGame.ParticleManagement.Core;
using BlastGame.ParticleManagement.ParticleTypes;
using BlastGame.ServiceManagement;
using BlastGame.UI.Animations;
using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace BlastGame.UI.GoalManagement
{
    public class GoalUIItem : MonoBehaviour, IUIAnimationEndPoint, IAnimatableUIItem
    {
        private event Action OnPlayAnimation;

        [SerializeField] private Image _goalImage;
        [SerializeField] private TMP_Text _goalCountText;
        [SerializeField] private GameObject _goalCountParent;

        public GoalData GoalData { get; private set; }

        private int _currentCount;

        public Vector3 GetPosition() => transform.position;

        private IParticleManager _particleManager;

        public void Initialize(GoalData data)
        {
            _particleManager = ServiceLocator.Instance.GetService<IParticleManager>();

            GoalData = data;

            _goalImage.sprite = data.ItemData.GetInitialSprite();

            _currentCount = data.RequireToComplete;

            UpdateText();
        }

        private void UpdateText()
        {
            _goalCountText.SetText(_currentCount.ToString());
        }

        public void OnReached()
        {
            _currentCount--;
            if (_currentCount <= 0)
            {
                _currentCount = 0;
                _goalCountParent.SetActive(false);
            }

            UpdateText();

            _particleManager.PlayParticle(typeof(GoalParticle), transform.position);

            OnPlayAnimation?.Invoke();
        }

        public void AddListener(Action listener)
        {
            OnPlayAnimation += listener;
        }
    }
}
