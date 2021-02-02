using System.Collections.ObjectModel;
using System.Linq;
using System.IO;
using System;

namespace Lesson_5
{
    public class Department
    {
        public string Name { get; set; }
        public ObservableCollection<Employee> LEmployees = new ObservableCollection<Employee>();

        public Department(string file_name)
        {
            using (FileStream f = new FileStream(file_name, FileMode.Open))
            {
                StreamReader sr = new StreamReader(f);
                string line;
                while ((line = sr.ReadLine()) != null)
                {
                    string[] s = line.Split(' ');
                    if (s.Length != 3)
                        continue;
                    Employee new_emp = new Employee(s[0], int.Parse(s[1]), double.Parse(s[2]));
                    if (!ContainsEmplpyee(new_emp))
                    {
                        LEmployees.Add(new_emp);
                    }
                }
                sr.Close();
            }
            if (LEmployees.Count > 0)
                Name = file_name.Remove(file_name.IndexOf('.'));
        }
        public void AddEmployee(string name, int age, double salary)
        {
            Employee new_emp = new Employee(name, age, salary);
            if (!ContainsEmplpyee(new_emp))
            {
                LEmployees.Add(new_emp);
            }
            //else
                //throw new System.DuplicateWaitObjectException("Такой сотрудник(имя, возраст и зарплата) уже существует в департаменте");
        }
        public override string ToString()
        {
            string ret = "";
            foreach (Employee emp in LEmployees)
                ret += emp.ToString() + "\n";
            return ret;
        }

        private bool ContainsEmplpyee(Employee new_emp)
        {
            return LEmployees.Any(emp => emp.Name == new_emp.Name
                && emp.Salary == new_emp.Salary
                && emp.Age == new_emp.Age);
        }
        public void RemoveEmployee(int id_emp)
        {
            for (int i = 0; i < LEmployees.Count(); i++)
            {
                if (LEmployees[i].IdEmp == id_emp)
                {
                    LEmployees.Remove(LEmployees[i]);
                    break;
                }
            }
        }
        public void UpdateEmployee(int id_emp, string name_new, int age_new, double salary_new)
        {
            for (int i = 0; i < LEmployees.Count(); i++)
            {
                if (LEmployees[i].IdEmp == id_emp)
                {
                    LEmployees[i].UpdateEmployee(name_new, age_new, salary_new);
                    break;
                }
            }
        }
    }
}
