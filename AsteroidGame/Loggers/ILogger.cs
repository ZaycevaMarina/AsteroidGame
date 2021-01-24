using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AsteroidGame.Loggers
{
    internal interface ILogger
    {
        public void Log(string Message);

        public void LogInformation(string Message);

        public void LogWarning(string Message);

        public void LogError(string Message);

        public void LogCritical(string Message);

        public void Flush();
    }
}
