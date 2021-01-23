using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AsteroidGame.Loggers
{
    internal abstract class Logger : ILogger
    {
        public abstract void Log(string Message);

        public void LogInformation(string Message)
        {
            Log($"{DateTime.Now:s}[info]:{Message}");
        }

        public void LogWarning(string Message)
        {
            Log($"{DateTime.Now:s}[warning]:{Message}");
        }

        public void LogError(string Message)
        {
            Log($"{DateTime.Now:s}[error]:{Message}");
        }

        public void LogCritical(string Message)
        {
            Log($"{DateTime.Now:s}[critical]:{Message}");
        }

        public abstract void Flush();
    }
}
