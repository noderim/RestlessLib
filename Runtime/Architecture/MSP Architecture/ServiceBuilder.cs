namespace RestlessLib.Architecture
{
    /// <summary>
    /// Class for building services using the builder pattern.
    /// </summary>
    /// <typeparam name="TService">The type of service to build.</typeparam>
    public class ServiceBuilder<TService> : ServiceBuilderBase<TService, ServiceBuilder<TService>>
    {
        public ServiceBuilder(MonoServiceProvider<TService> msp) : base(msp)
        {

        }
    }
}