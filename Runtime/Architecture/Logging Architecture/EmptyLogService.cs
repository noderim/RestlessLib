namespace RestlessLib.Architecture
{
    public class EmptyLogService : ILogService
    {
        public bool EnableLogging { get; set; } = true;

        public void Log(string message)
        {
            // Do nothing
        }

        public void LogError(string message)
        {

        }

        public void LogWarning(string message)
        {

        }
    }
}