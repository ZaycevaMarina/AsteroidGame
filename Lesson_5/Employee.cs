namespace Lesson_6_Binding_Trigger
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

        public void UpdateEmployee(string name, int age, double salary)
        {
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
