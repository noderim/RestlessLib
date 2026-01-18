namespace RestlessLib.Architecture
{
    /// <summary>
    /// Interface for service dependencies.
    /// </summary>
    /// <typeparam name="TDependency">The type of dependency.</typeparam>
    public interface IServiceDependency<TDependency>
    {
        public TDependency Dependency { get; set; }
        public void SetDependencies(TDependency dependencies);
        public bool CheckDependencies();
    }
}