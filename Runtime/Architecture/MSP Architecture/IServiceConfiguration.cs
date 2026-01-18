public interface IServiceConfiguration<TConfig>
{
    public TConfig Config { get; set; }
    void ConfigureService(TConfig config);
}
