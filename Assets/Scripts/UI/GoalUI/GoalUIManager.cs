using BlastGame.EventSystem;
using BlastGame.GoalSystem;
using BlastGame.LevelManagement.Datas;
using BlastGame.ObjectPoolingSystem;
using BlastGame.ServiceManagement;
using BlastGame.UI.Animations;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace BlastGame.UI.GoalManagement
{
    public class GoalUIManager : MonoBehaviour
    {
        [SerializeField] private GoalUIItem _goalMenuItemPrefab;
        [SerializeField] private GoalAnimationVisualBase _goalAnimationVisualPrefab;
        [SerializeField] private Transform _goalMenuParent;
        List<GoalUIItem> _goalMenuItems = new();

        private void Awake()
        {
            GenerateGoalMenuVisuals(ServiceLocator.Instance.GetService<IGoalProvider>().GetGoals());

            SubscribeToEvents();
        }

        private void GenerateGoalMenuVisuals(List<GoalData> goals)
        {
            if (goals.Count <= 0)
            {
                return;
            }

            foreach (GoalData goal in goals)
            {
                GoalUIItem goalMenuItem = Instantiate(_goalMenuItemPrefab, _goalMenuParent);

                goalMenuItem.Initialize(goal);

                _goalMenuItems.Add(goalMenuItem);
            }
        }

        private void SubscribeToEvents()
        {
            EventManager.Instance.AddListener<GoalItemBeingProcessedEvent>(OnItemIsBeingProcessed);
        }

        private void OnItemIsBeingProcessed(object eventData)
        {
            ItemCollectionData itemCollectionData = ((GoalItemBeingProcessedEvent)eventData).ItemCollectionData;

            GoalUIItem releventGoalItem = _goalMenuItems.FirstOrDefault(menuItem => menuItem.GoalData.ItemData == itemCollectionData.ItemData);

            if (releventGoalItem == null)
            {
                return;
            }

            GoalAnimationVisualBase animationVisual = ServiceLocator.Instance.GetService<IObjectPoolManager>().GetObject(_goalAnimationVisualPrefab);

            animationVisual.transform.SetParent(transform);

            animationVisual.transform.position = itemCollectionData.Position;

            animationVisual.Initialize(itemCollectionData.ItemData, releventGoalItem);

            ServiceLocator.Instance.GetService<UIAnimationCommandManager>().RegisterAndProcessCommand(animationVisual);
        }

        private void UnSubscribeToEvents()
        {
            EventManager.Instance.RemoveListener<GoalItemBeingProcessedEvent>(OnItemIsBeingProcessed);
        }

        private void OnDestroy()
        {
            UnSubscribeToEvents();
        }
    }
}
