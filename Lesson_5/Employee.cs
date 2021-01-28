using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lesson_5
{
    public class Employee
    {
        protected static int __IdEmp = 1;
        public int IdEmp { get; set; }
        public string Name { get; set; }
        public int Age { get; set; }
        public double Salary { get; set; }

        public Employee(string name, int age, double salary)
        {
            IdEmp = __IdEmp++;
            Name = name;
            Age = age;
            Salary = salary;
        }
        public override string ToString()
        {
            return $"{IdEmp}\t{Name}\t{Age}\t{Salary}";
        }
    }
}
