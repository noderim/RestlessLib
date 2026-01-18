namespace RestlessLib.Architecture
{
    /// <summary>
    /// Interface for service configuration.
    /// </summary>
    /// <typeparam name="TConfig">The type of configuration.</typeparam>
    public interface IServiceConfiguration<TConfig>
    {
        public TConfig Config { get; set; }
        void ConfigureService(TConfig config);
    }
}