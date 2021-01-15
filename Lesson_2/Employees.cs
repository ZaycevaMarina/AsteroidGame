using System;
using System.Collections.Generic;
using System.Text;

namespace Lesson_2
{
    class Employees
    {
        private readonly Employee[] MEmployees;

        public Employees(int count_employees)
        {
            MEmployees = new Employee[count_employees];
            Random rnd = new Random();
            double payment;
            for (int i = 0; i < count_employees; i++)
            {
                payment = rnd.NextDouble();
                if (i % 2 == 0)
                    MEmployees[i] = new EmployeeHourlyPayment(payment * 1_000);
                else
                    MEmployees[i] = new EmployeeFixedPayment(payment * 200_000);
            }
            Array.Sort(MEmployees);
        }

        public void Print()
        {
            foreach (Employee emp in MEmployees)
                Console.WriteLine(emp.ToString());
        }

    }
}
