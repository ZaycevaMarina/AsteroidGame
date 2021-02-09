using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebAPI.Models
{
    public class Department
    {
        public int Id { get; /*private*/ set; }
        public string Name { get; /*private*/ set; }
        public ICollection<Employee> Employees { get; /*private*/ set; }
        //public Department(int id, string name, IEnumerable<Employee> employees)
        //{
        //    Id = id;
        //    Name = name;
        //    Employees = employees;
        //}
    }
}
