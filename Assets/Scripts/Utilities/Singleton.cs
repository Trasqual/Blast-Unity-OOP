using UnityEngine;

namespace BlastGame.Utilities
{
    public class Singleton<T> : MonoBehaviour where T : MonoBehaviour
    {
        public static T Instance;

        protected virtual void Awake()
        {
            if (Instance != null)
            {
                Destroy(Instance.gameObject);
            }

            Instance = this as T;
        }
    }
}
