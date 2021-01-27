/* Дана коллекция List<T>. 
 * Требуется подсчитать, сколько раз каждый элемент встречается в данной коллекции:
- для целых чисел;
- для обобщенной коллекции;
- *используя Linq.*/
using System;
using System.Collections.Generic;
using System.Linq;

namespace Lesson_4
{
    class Program
    {
        static void Main(string[] args)
        {
            //а)
            Console.WriteLine("а) Подсчёт количества встречаемости в List<int>");
            List<int> List_int = new List<int>();
            int len = 100;
            Random rnd = new Random();
            for(int i = 0; i < len; i++)
                List_int.Add(rnd.Next(0, 10));
            Dictionary<int, int> Frequency = new Dictionary<int, int>();
            foreach (int count in List_int)
            {
                if (!Frequency.ContainsKey(count))
                    Frequency[count] = 1;
                else
                    Frequency[count]++;
            }
            var frequency = Frequency.OrderBy(pair => pair.Key);
            foreach(var fr in frequency)
            {
                Console.WriteLine($"Число {fr.Key} встречается {fr.Value} раз");
            }
            //б)
            Console.WriteLine("б) Для обобщенной коллекции на примере List<double> в классе TList<T>");
            TList<double> T_List = new TList<double>();
            T_List.T_List = new List<double>();
            for (int i = 0; i < len; i++)
                T_List.T_List.Add((double)rnd.Next(0,10)/10);
            T_List.CalculateFrequency();
            T_List.PrintFrequency();
            
            //в)
            Console.WriteLine("в) используя Linq");
            T_List.CalculateFrequencyLinq();
            T_List.PrintFrequency();

            Console.WriteLine("Для завершения нажмите любую кнопку...");
            Console.ReadKey();
        }
    }

}
