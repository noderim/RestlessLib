using UnityEngine;

namespace RestlessLib
{
    /// <summary>
    /// Singleton Base class to be inherited by other classes
    /// </summary>
    public abstract class MonoSingleton<T> : MonoBehaviour where T : MonoSingleton<T>
    {
        public static T Instance { get; private set; }
        public static bool InstanceAvailable => Instance != null;

        protected virtual void Awake()
        {
            if (Instance != null && Instance != this)
            {
                Destroy(gameObject); // Avoid duplicates
                return;
            }

            Instance = this as T;
        }

        protected virtual void OnDestroy()
        {
            if (Instance == this)
            {
                Instance = null;
            }
        }
        public bool CheckInstance()
        {
            return Instance != null;
        }
        public bool CheckInstance(string caller)
        {
            if (Instance == null)
            {
                Debug.LogWarning($"[{caller}] - Instance of {typeof(T).Name} is null.");
            }
            return Instance != null;
        }
        public static T GetInstance()
        {
            return Instance;
        }

    }
}
