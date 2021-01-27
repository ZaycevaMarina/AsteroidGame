using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Lesson_4
{
    class TList<T>
    {
        public List<T> T_List { get; set; }
        Dictionary<T, int> Frequency;
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
            var frequency = Frequency.OrderBy(pair => pair.Key);
            foreach (var fr in frequency)
                Console.WriteLine($"Элемент {fr.Key} встречается {fr.Value} раз");
        }

        public void CalculateFrequencyLinq()
        {
            if (T_List == null)
                return;
            var group = from count in T_List
                        orderby count
                        group count by count;
            foreach (var fr in group)
                Console.WriteLine($"Элемент {fr.Key} встречается {fr.Count()} раз");
        }
    }
}
