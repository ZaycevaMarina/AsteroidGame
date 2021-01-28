using System;
using System.Collections.Generic;
using System.Linq;
using System.IO;

namespace Lesson_5
{
    public class Department
    {
        public List<List<int>> IdsDep { get; private set; }

        List<Employee> LEmployees = new List<Employee>();
        public Department()
        {
            IdsDep = new List<List<int>>();
        }

        public Department(string file_name)
        {
            IdsDep = new List<List<int>>();
            FileStream f = new FileStream(file_name, FileMode.Open);
            StreamReader sr = new StreamReader(f);
            string line;
            int id_dep;
            while ((line = sr.ReadLine()) != null)
            {
                string[] s = line.Split(' ');
                if (s.Length != 4)
                    continue;
                id_dep = int.Parse(s[0]);
                Employee new_emp = new Employee(id_dep, s[1], int.Parse(s[2]), double.Parse(s[3]));
                if (!ContainsEmplpyee(new_emp))
                {
                    if (IdsDep.Count >= id_dep && IdsDep.Count > 0)
                    {
                        if (!IdsDep[id_dep - 1].Contains(new_emp.IdEmp))
                        {
                            IdsDep[id_dep - 1].Add(new_emp.IdEmp);
                            LEmployees.Add(new_emp);
                        }
                    }
                    else
                    {
                        List<int> new_dep = new List<int>();
                        new_dep.Add(new_emp.IdEmp);
                        IdsDep.Add(new_dep);
                        LEmployees.Add(new_emp);
                    }
                }
            }
            f.Close();
        }

        public void AddEmployee(Employee emp)
        {
            if (!ContainsEmplpyee(emp))
            {
                if (IdsDep.Count >= emp.IdDep && IdsDep.Count > 0)
                {
                    if (!IdsDep[emp.IdDep - 1].Contains(emp.IdEmp))
                    {
                        IdsDep[emp.IdDep - 1].Add(emp.IdEmp);
                        LEmployees.Add(emp);
                    }
                }
                else
                {
                    List<int> new_dep = new List<int>();
                    new_dep.Add(emp.IdEmp);
                    IdsDep.Add(new_dep);
                    LEmployees.Add(emp);
                }
            }
        }
        public override string ToString()
        {
            string ret = "";
            int id = 0;
            foreach (List<int> dep in IdsDep)
            {
                ret += $"Отдел №{id + 1}:\n";
                foreach (int id_emp in dep)
                    ret += SearchEmployeeByIdDepartment(id_emp) + "\n";
                ret += "\n";
                id++;
            }
            return ret;
        }
         private string SearchEmployeeByIdDepartment(int id_emp)
        {
            string s = "";
            foreach(Employee emp in LEmployees)
            {
                if (emp.IdEmp == id_emp)
                {
                    s = emp.ToString();
                    break;
                }
            }
            return s;
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
    }
}
