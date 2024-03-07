using BlastGame.ServiceManagement;
using UnityEngine;

namespace BlastGame.ObjectPoolingSystem
{
    public interface IObjectPoolManager : IService
    {
        public T GetObject<T>(T prefab) where T : MonoBehaviour;
        public void ReleaseObject<T>(T pooledObject) where T : MonoBehaviour;
        public void PrePopulatePool<T>(T prefab, int count) where T : MonoBehaviour;
    }
}
