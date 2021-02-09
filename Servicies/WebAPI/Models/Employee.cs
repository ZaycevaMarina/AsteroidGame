using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebAPI.Models
{
    public class Employee
    {
        public int Id { get; /*private*/ set; }
        public string Name { get; /*private*/ set; }
        public int Age { get; /*private*/ set; }
        public double Salary { get; /*private*/ set; }
        //public int IdDep { get; private set; }

        //public Employee(int id, string name, int age, double salary/*, int id_dep*/)
        //{
        //    Id = id;
        //    Name = name; 
        //    Age = age;
        //    Salary = salary;
        //    //IdDep = id_dep;
        //}
    }
}
