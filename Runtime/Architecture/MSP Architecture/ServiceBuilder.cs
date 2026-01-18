public class ServiceBuilder<TService> : ServiceBuilderBase<TService, ServiceBuilder<TService>>
{
    public ServiceBuilder(MonoServiceProvider<TService> msp) : base(msp)
    {

    }
}