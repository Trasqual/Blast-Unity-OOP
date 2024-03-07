using BlastGame.AudioManagement;
using BlastGame.BoardItems.Core;
using BlastGame.EventSystem;
using BlastGame.GoalSystem;
using BlastGame.ParticleManagement.Core;
using BlastGame.ParticleManagement.ParticleTypes;
using BlastGame.ServiceManagement;
using UnityEngine;
using AudioType = BlastGame.AudioManagement.AudioType;

namespace BlastGame.BoardItems.Strategies.PoppingStrategies
{
    [CreateAssetMenu(fileName = "BlockPoppingStrategy", menuName = "Strategies/PoppingStrategy/BlockPoppingStrategy")]
    public class BlockPoppingStrategy : PoppingStrategy
    {
        public override bool CanBePopped(object poppedBy)
        {
            return true;
        }

        public override void ExecuteStrategy(BoardItem item)
        {
            ItemCollectionData itemCollectionData = new ItemCollectionData()
            {
                ItemData = item.GetData(),
                Position = item.transform.position,
                AudioData = AudioData.Create(AudioType.Collect_UI)
            };
            EventManager.Instance.TriggerEvent<ItemCollectedEvent>(new ItemCollectedEvent { ItemCollectionData = itemCollectionData });

            ServiceLocator.Instance.GetService<IParticleManager>().PlayParticle(typeof(BlockParticle), item.transform.position, data: item.GetData());
            ServiceLocator.Instance.GetService<IAudioManager>().PlayWithSuppressDurationAsync(AudioData.Create(AudioType.Block_Pop).WithVolume(0.6f), 100);

            item.ClearAndReleaseToPool();
        }
    }
}
