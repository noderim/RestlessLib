namespace RestlessLib.Architecture
{
    /// <summary>
    /// Interface for logging user.
    /// </summary>
    public interface ILogUser
    {
        public ILogService LogService { get; set; }
        public bool EnableLogging { get; set; }
    }
}