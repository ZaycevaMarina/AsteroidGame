/* Дан фрагмент программы:
 а) Свернуть обращение к OrderBy с использованием лямбда-выражения =>.
 б) Развернуть обращение к OrderBy с использованием делегата. */
using System;
using System.Collections.Generic;
using System.Linq;

namespace Lesson_4_3
{
    class Program
    {
    static void Main(string[] args)
        {
            Console.WriteLine("Словарь");
            #region Program
            Dictionary<string, int> dict = new Dictionary<string, int>()
                  {
                    { "four", 4 },
                    { "two", 2 },
                    { "one", 1 },
                    { "three", 3 },
                  };
            var d = dict.OrderBy(delegate (KeyValuePair<string, int> pair) { return pair.Value; });
            foreach (var pair in d)
                Console.WriteLine("{0} - {1}", pair.Key, pair.Value);
            #endregion
            
            Console.WriteLine("а) Свернуть обращение к OrderBy с использованием лямбда-выражения =>.");
            d = dict.OrderBy(pair => pair.Value);
            foreach (var pair in d)
                Console.WriteLine("{0} - {1}", pair.Key, pair.Value);
            
            Console.WriteLine("С использованием Linq");
            var kort = from pair in dict
                       orderby pair.Value ascending
                       select pair;
            foreach (var pair in kort)
                Console.WriteLine("{0} - {1}", pair.Key, pair.Value);

            Console.WriteLine("Для продолжения нажмите любую кнопку...");
            Console.ReadKey();
        }
    }
}
