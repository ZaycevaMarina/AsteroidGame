using System.Collections.Generic;

namespace Lesson_8_WebAPIClient
{
    public class Department
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public ICollection<Employee> Employees { get; set; }
    }
}
