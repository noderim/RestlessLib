using RestlessLib.Architecture;
using Sirenix.OdinInspector;
using UnityEngine;

public abstract class MonoServiceProvider<TService> : MonoBehaviour
{
    [ShowInInspector]
    public TService service;
    public abstract bool TryCreateService(out TService service);
    public LifecycleBinder lifecycleBinder = new LifecycleBinder();

    public void Bind(TService _passedService)
    {
        lifecycleBinder.Bind(_passedService);
    }

    // Entry point for the Fluent API
    public virtual ServiceBuilder<TService> CreateService()
    {
        return new ServiceBuilder<TService>(this);
    }
}
