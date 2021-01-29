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
            FileStream f = new FileStream(file_name, FileMode.Open);
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
            f.Close();
            if (LEmployees.Count > 0)
                Name = file_name.Remove(file_name.IndexOf('.'));
        }
        public void AddEmployee(string emp_str)
        {
            string[] s = emp_str.Split(' ');
            if (s.Length != 3)
                return;
            Employee new_emp = new Employee(s[0], int.Parse(s[1]), double.Parse(s[2]));
            if (!ContainsEmplpyee(new_emp))
            {
                LEmployees.Add(new_emp);
            }
            else
                throw new Exception("Такой сотрудник(имя, возраст и зарплата) уже существует в департаменте");
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
            foreach(Employee emp in LEmployees)
            {
                if (emp.Name == new_emp.Name && emp.Salary == new_emp.Salary && emp.Age == new_emp.Age)
                    return true;
            }
            return false; ;
        }

        public void RemoveEmployee(string emp_to_remove)
        {
            string[] s = emp_to_remove.Split('\t');
            if (s.Length != 4)
                return;
            for (int i = 0; i < LEmployees.Count(); i++)
            {
                if (LEmployees[i].Name == s[1] && LEmployees[i].Age == int.Parse(s[2]) && LEmployees[i].Salary == double.Parse(s[3]))//s[0] = id
                    LEmployees.Remove(LEmployees[i]);
            }
        }

        public void UpdateEmployee(string emp_to_update, string new_emp)
        {
            string[] s = emp_to_update.Split('\t');
            string[] s_new = new_emp.Split('\t');
            if (s.Length != 4)
                return;
            if (s_new.Length != 4)
                return;
            for (int i = 0; i < LEmployees.Count(); i++)
            {
                if (LEmployees[i].Name == s[1] && LEmployees[i].Age == int.Parse(s[2]) && LEmployees[i].Salary == double.Parse(s[3]))//s[0] = id
                    LEmployees[i].UpdateEmployee(s_new[1], int.Parse(s_new[2]), double.Parse(s_new[3]));
            }
        }
    }
}
