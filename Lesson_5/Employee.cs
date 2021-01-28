using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lesson_5
{
    public class Employee
    {
        static int id = 0;
        public int Id { get; private set; }
        public string Name { get; private set; }
        public int Age { get; private set; }
        public double Salary { get; private set; }

        public Employee(string name, int age, double salary)
        {
            Id = id++;
            Name = name;
            Age = age;
            Salary = salary;
        }
        public override string ToString()
        {
            return $"{Id}\t{Name}\t{Age}\t{Salary}";
        }
    }
}
