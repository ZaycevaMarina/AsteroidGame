//Добавить в пример Lesson3 обобщенный делегат
using System;

namespace Lesson_3
{
    public delegate void TMyDelegate<T>(T arg);
    class Source
    {
        public event TMyDelegate<object> Run;

        public void Start()
        {
            Console.WriteLine("RUN");
            Run?.Invoke(this);
        }
    }
    class Observer1 // Наблюдатель 1
    {
        public void Do(object o)
        {
            Console.WriteLine("Первый. Принял, что объект {0} побежал", o);
        }
    }
    class Observer2 // Наблюдатель 2
    {
        public void Do(object o)
        {
            Console.WriteLine("Второй. Принял, что объект {0} побежал", o);
        }
    }
    class Program
    {
        static void Main(string[] args)
        {
            Source s = new Source();
            Observer1 o1 = new Observer1();
            Observer2 o2 = new Observer2();
            TMyDelegate<object> td = new TMyDelegate<object>(o1.Do);
            s.Run += td;
            s.Run += o2.Do;
            s.Start();
            s.Run -= o1.Do;
            s.Start();

            Console.WriteLine("Для завершения нажмите любую кнопку...");
            Console.ReadKey();
        }
    }
}
