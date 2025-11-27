using UnityEngine;

namespace RestlessLib.Architecture
{
    public abstract class MonoConfigurableService<TService, TConfig> : MonoService<TService>
    {
        [SerializeField] private TConfig _config;

        public TConfig Config => _config;

        // If config is missing in Inspector, don't auto-init. Wait for Bootstrapper.
        protected override bool AutoInitialize => _config != null && !_config.Equals(null);

        /// <summary>
        /// Call this from the Bootstrapper to inject config and start the service.
        /// </summary>
        public void Bootstrap(TConfig config)
        {
            _config = config;
            InitializeService();
        }

        protected sealed override TService CreateService()
        {
            if (_config == null || _config.Equals(null))
            {
                Debug.LogError($"[{GetType().Name}] Config is missing! Did you forget to Bootstrap it?");
                return default;
            }
            return CreateWithConfig(_config);
        }

        public abstract TService CreateWithConfig(TConfig config);
    }
}
