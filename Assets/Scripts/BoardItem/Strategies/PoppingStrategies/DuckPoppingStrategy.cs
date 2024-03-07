using BlastGame.AudioManagement;
using BlastGame.BoardItems.Core;
using BlastGame.EventSystem;
using BlastGame.GoalSystem;
using BlastGame.PowerUpSystem;
using BlastGame.ServiceManagement;
using UnityEngine;
using AudioType = BlastGame.AudioManagement.AudioType;

namespace BlastGame.BoardItems.Strategies.PoppingStrategies
{
    [CreateAssetMenu(fileName = "DuckPoppingStrategy", menuName = "Strategies/PoppingStrategy/DuckPoppingStrategy")]
    public class DuckPoppingStrategy : PoppingStrategy
    {
        public override bool CanBePopped(object poppedBy)
        {
            return poppedBy is not IPowerUp;
        }

        public override void ExecuteStrategy(BoardItem item)
        {
            ItemCollectionData itemCollectionData = new ItemCollectionData()
            {
                ItemData = item.GetData(),
                Position = item.transform.position
            };

            EventManager.Instance.TriggerEvent<ItemCollectedEvent>(new ItemCollectedEvent { ItemCollectionData = itemCollectionData });

            EventManager.Instance.TriggerEvent<ReleaseGameLoopEvent>();

            ServiceLocator.Instance.GetService<IAudioManager>().Play(AudioData.Create(AudioType.Duck_Pop));

            item.ClearAndReleaseToPool();
        }
    }
}
