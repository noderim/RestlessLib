using UnityEngine;

namespace RestlessLib.Architecture
{
    public class UnityLogService : ILogService
    {
        public bool EnableLogging { get; set; } = true;

        public void Log(string message)
        {
            if (!EnableLogging) return;
            Debug.Log($"{message}");
        }

        public void LogError(string message)
        {
            if (!EnableLogging) return;
            Debug.LogError($"{message}");
        }

        public void LogWarning(string message)
        {
            if (!EnableLogging) return;
            Debug.LogWarning($"{message}");
        }

    }
}