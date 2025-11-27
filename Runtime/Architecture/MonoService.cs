using UnityEngine;

namespace RestlessLib.Architecture
{
    public abstract class MonoService<TService> : MonoBehaviour
    {
        public TService Service { get; private set; }
        public bool IsInitialized { get; private set; }

        private ITickable _tickable;
        private IFixedTickable _fixedTickable;

        protected abstract TService CreateService();

        // Allow child classes to disable auto-init (for Bootstrapper usage)
        protected virtual bool AutoInitialize => true;

        protected virtual void Awake()
        {
            if (AutoInitialize)
            {
                InitializeService();
            }
        }

        // Public method so Bootstrapper can call it
        public void InitializeService()
        {
            if (IsInitialized) return;

            Service = CreateService();

            // 1. Handle Initialization
            if (Service is IInitializable initializable)
            {
                initializable.Initialize();
            }

            // 2. Handle Ticking (Cache the cast)
            _tickable = Service as ITickable;
            _fixedTickable = Service as IFixedTickable;

            SystemLocator.Register(Service);
            IsInitialized = true;
        }

        protected virtual void Update()
        {
            if (IsInitialized) _tickable?.Tick(Time.deltaTime);
        }

        protected virtual void FixedUpdate()
        {
            if (IsInitialized) _fixedTickable?.FixedTick(Time.fixedDeltaTime);
        }

        protected virtual void OnDestroy()
        {
            if (!IsInitialized) return;

            SystemLocator.Unregister(Service);

            // 3. Handle Cleanup
            if (Service is IDisposable disposable)
            {
                disposable.Dispose();
            }
        }
    }
}
