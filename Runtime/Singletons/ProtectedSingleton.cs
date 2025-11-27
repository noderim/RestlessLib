using UnityEngine;

namespace RestlessLib
{
    /// <summary>
    /// Singleton Base class to be inherited by other classes. Have its instance as protected.
    /// </summary>
    public abstract class ProtectedSingleton<T> : MonoBehaviour where T : ProtectedSingleton<T>
    {
        protected static T Instance { get; private set; }
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
        public static bool CheckInstance()
        {
            return Instance != null;
        }
        public static bool CheckInstance(string caller)
        {
            if (Instance == null)
            {
                Debug.LogWarning($"[{caller}] - Instance of {typeof(T).Name} is null.");
            }
            return Instance != null;
        }
    }
}
