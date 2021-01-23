using System;
using System.IO;

namespace AsteroidGame.Loggers
{
    internal class TextFileLogger : Logger, IDisposable
    {
        private readonly TextWriter _Writer;

        public TextFileLogger(string FileName)
        {
            _Writer = File.CreateText(FileName);
            //((StreamWriter) _Writer).AutoFlush = true;
        }

        public override void Log(string Message)
        {
            _Writer.WriteLine(Message);
        }

        public override void Flush()
        {
            _Writer.Flush();
        }

        public void Dispose()
        {
            Flush();
            _Writer.Dispose();
        }
    }
}
