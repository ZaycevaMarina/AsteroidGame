using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Lesson_4
{
    class TList<T>
    {
        public List<T> T_List { get; set; }
        int len = 100;
        Dictionary<T, int> Frequency;

        //public TList()
        //    {
        //        //for (int i = 0; i < len; i++)
        //        //    T_List.Add(0);
        //    }
        public void CalculateFrequency()
        {
            if (T_List == null)
                return;
            Frequency = new Dictionary<T, int>();
            foreach (T count in T_List)
            {
                if (!Frequency.ContainsKey(count))
                    Frequency[count] = 1;
                else
                    Frequency[count]++;
            }
        }

        public void PrintFrequency()
        {
            if (Frequency == null)
                return;
            foreach (var fr in Frequency)
            {
                Console.WriteLine($"Элемент {fr.Key} встречается {fr.Value} раз");
            }
        }

        public void CalculateFrequencyLinq()
        {
            if (T_List == null)
                return;
            var group = from count in T_List
                        group count by count;
        }
    }
}
