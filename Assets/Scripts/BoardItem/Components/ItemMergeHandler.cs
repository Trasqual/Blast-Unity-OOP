using BlastGame.AudioManagement;
using BlastGame.EventSystem;
using BlastGame.GoalSystem;
using BlastGame.ServiceManagement;
using BlastGame.StaticDatas;
using DG.Tweening;
using System.Threading.Tasks;
using UnityEngine;
using AudioType = BlastGame.AudioManagement.AudioType;

namespace BlastGame.BoardItems.Components
{
    public class ItemMergeHandler : BoardItemComponentBase
    {
        public async Task Merge(Vector3 mergeCenter)
        {
            ItemCollectionData collectionData = new ItemCollectionData()
            {
                ItemData = _attachedItem.GetData(),
                Position = mergeCenter,
                AudioData = AudioData.Create(AudioType.Block_Pop)
            };

            await _attachedItem.transform.DOMove(mergeCenter, StaticGameData.MERGE_DURATION).SetEase(Ease.InBack).OnComplete(() =>
            {
                _attachedItem.ClearAndReleaseToPool();

                ServiceLocator.Instance.GetService<IAudioManager>().PlayWithSuppressDurationAsync(AudioData.Create(AudioType.Block_Pop).WithVolume(0.6f), 100);

                EventManager.Instance.TriggerEvent<ItemCollectedEvent>(new ItemCollectedEvent { ItemCollectionData = collectionData });

            }).AsyncWaitForCompletion();
        }
    }
}
