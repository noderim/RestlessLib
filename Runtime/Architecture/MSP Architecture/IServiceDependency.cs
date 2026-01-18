public interface IServiceDependency<TDependency>
{
    public TDependency Dependency { get; set; }
    public void SetDependencies(TDependency dependencies);
    public bool CheckDependencies();
}
