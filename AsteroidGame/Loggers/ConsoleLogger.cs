using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AsteroidGame.Loggers
{
    internal class ConsoleLogger : Logger
    {
        public override void Log(string Message)
        {
            Console.WriteLine(Message);
        }

        public override void Flush()
        {
            Console.WriteLine("Все данные логгера сохранены");
        }
    }
}
