namespace RestlessLib.Architecture
{
    /// <summary>
    /// Interface for logger service.
    /// </summary>
    public interface ILogService
    {
        public bool EnableLogging { get; set; }
        void Log(string message);
        void LogWarning(string message);
        void LogError(string message);
    }
}