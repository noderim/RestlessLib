using System;
using UnityEngine;

namespace RestlessLib.Architecture
{
    /// <summary>
    /// Base class for building services using the builder pattern.
    /// </summary>
    /// <typeparam name="TService">The type of service to build.</typeparam>
    /// <typeparam name="TBuilder">The type of the builder itself.</typeparam>
    public class ServiceBuilderBase<TService, TBuilder> : IServiceBuilder<TService> where TBuilder : ServiceBuilderBase<TService, TBuilder>
    {
        private readonly MonoServiceProvider<TService> _msp;
        private Action<TService> _onSuccess;
        private Action _onFailure;
        private bool _asSingleton;
        ILogService _logService;

        public ServiceBuilderBase(MonoServiceProvider<TService> msp)
        {
            _msp = msp;
        }

        public virtual TBuilder WithConfig<TConfig>(TConfig config)
        {
            if (_msp is IServiceConfiguration<TConfig> configurable)
            {
                Debug.Log($"Configuring service {_msp.GetType().Name} with provided config: {config}.");
                configurable.Config = config;
                configurable.ConfigureService(config);
            }
            else
            {
                Debug.LogWarning($"MSP {_msp.GetType().Name} does not implement IServiceConfgure<{typeof(TConfig).Name}>");
            }
            return (TBuilder)this;
        }

        // Allows modifying the config that was just set (or the default one)
        public virtual TBuilder OverrideConfig<TConfig>(Action<TConfig> overrideAction)
        {
            if (_msp is IServiceConfiguration<TConfig> configurable)
            {
                overrideAction?.Invoke(configurable.Config);
                // Re-apply configuration after override if necessary
                configurable.ConfigureService(configurable.Config);
            }
            return (TBuilder)this;
        }

        public virtual TBuilder DontDestroyOnLoad()
        {
            UnityEngine.Object.DontDestroyOnLoad(_msp.gameObject);
            return (TBuilder)this;
        }

        public virtual TBuilder WithDependencies<TDependency>(TDependency dependencies)
        {
            if (_msp is IServiceDependency<TDependency> dependent)
            {
                Debug.Log($"Setting dependencies for service {_msp.GetType().Name} with provided dependencies: {dependencies}.");
                dependent.SetDependencies(dependencies);
            }
            else
            {
                Debug.LogWarning($"MSP {_msp.GetType().Name} does not implement IServiceDependency<{typeof(TDependency).Name}>");
            }
            return (TBuilder)this;
        }

        public virtual TBuilder OnSuccess(Action<TService> callback)
        {
            _onSuccess = callback;
            return (TBuilder)this;
        }

        public virtual TBuilder OnFailure(Action callback)
        {
            _onFailure = callback;
            return (TBuilder)this;
        }

        public virtual TBuilder WithLogging(ILogService logService)
        {
            _logService = logService;
            return (TBuilder)this;
        }

        public virtual TBuilder AsSingleton()
        {
            _asSingleton = true;
            return (TBuilder)this;
        }

        public virtual TService Create()
        {
            if (_msp.TryCreateService(out var service))
            {
                _msp.service = service;
                _msp.Bind(service);

                if (_logService != null && service is ILogUser ilogUser)
                {
                    ilogUser.EnableLogging = true;
                    ilogUser.LogService = _logService;
                    Debug.Log($"[ServiceBuilder] Logging enabled for service {typeof(TService).Name} using {_logService.GetType().Name}.");
                }

                if (_asSingleton)
                {
                    ServiceLocator.Register<TService>(service);
                    Debug.Log($"[ServiceBuilder] {typeof(TService).Name} registered in ServiceLocator as Singleton.");
                }

                _onSuccess?.Invoke(service);
                return service;
            }
            else
            {
                _onFailure?.Invoke();
                return default;
            }
        }
    }
}