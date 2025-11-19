namespace RestlessLib
{
    public abstract class MonoSingletonPersistent<T> : MonoSingleton<T> where T : MonoSingletonPersistent<T>
    {
        protected override void Awake()
        {
            if (Instance != null)
            {
                Destroy(gameObject);
                return;
            }
            DontDestroyOnLoad(gameObject);
            base.Awake();
        }
    }
}
