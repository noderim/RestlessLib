namespace RestlessLib.Architecture
{
    /// <summary>
    /// Interface for service builders.
    /// </summary>
    /// <typeparam name="TService">The type of service to build.</typeparam>
    public interface IServiceBuilder<TService>
    {
        TService Create();
    }
}