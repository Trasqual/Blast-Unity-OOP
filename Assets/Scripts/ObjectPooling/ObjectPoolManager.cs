using System;
using System.Collections.Generic;
using UnityEngine;

namespace BlastGame.ObjectPoolingSystem
{
    public class ObjectPoolManager : MonoBehaviour, IObjectPoolManager
    {
        private Dictionary<Type, Queue<GameObject>> _pools = new();

        public T GetObject<T>(T prefab) where T : MonoBehaviour
        {
            Queue<GameObject> pool = GetPool(prefab.GetType());

            if (pool == null)
            {
                pool = CreatePool(prefab);
            }

            if (pool.Count <= 0)
            {
                T spawn = Instantiate(prefab);

                spawn.transform.SetParent(transform);
                spawn.transform.localPosition = Vector3.zero;

                pool.Enqueue(spawn.gameObject);
            }

            GameObject pooledObject = pool.Dequeue();
            pooledObject.SetActive(true);

            T objectToReturn = pooledObject.GetComponent<T>();

            return objectToReturn ?? throw new Exception("Pooled item doesn't contain the requested component type!");
        }

        public void ReleaseObject<T>(T pooledObject) where T : MonoBehaviour
        {
            if (_pools.TryGetValue(pooledObject.GetType(), out Queue<GameObject> pool))
            {
                pooledObject.gameObject.SetActive(false);
                pool.Enqueue(pooledObject.gameObject);
            }
            else
            {
                Destroy(pooledObject.gameObject);
            }
        }

        private Queue<GameObject> GetPool(Type prefabType)
        {
            if (_pools.TryGetValue(prefabType, out Queue<GameObject> pool))
            {
                return pool;
            }

            return null;
        }

        private Queue<GameObject> CreatePool<T>(T prefab) where T : MonoBehaviour
        {
            Queue<GameObject> pool = new Queue<GameObject>();

            T spawn = Instantiate(prefab);

            spawn.gameObject.SetActive(false);

            pool.Enqueue(spawn.gameObject);

            _pools.Add(prefab.GetType(), pool);

            return pool;
        }

        public void PrePopulatePool<T>(T prefab, int count) where T : MonoBehaviour
        {
            CreatePool(prefab);

            for (int i = 1; i < count; i++)
            {
                T spawn = Instantiate(prefab);

                ReleaseObject(spawn);
            }
        }
    }
}
