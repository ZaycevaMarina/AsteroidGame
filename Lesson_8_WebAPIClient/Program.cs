using System;
using System.Net.Http;
using System.Net.Http.Json;
using System.Windows.Forms;
using Microsoft.Extensions.Configuration;

namespace Lesson_8_WebAPIClient
{
    static class Program
    {
        public static IConfiguration Configuration { get; } = new ConfigurationBuilder()
           .AddJsonFile("appsettings.json", false, true)
           .Build();
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            string[] department_names = { "Отдел 10", "Отдел 11", "Отдел 12" };
            string[] employee_names = { "Кузнецов", "Попов", "Тараканов" };

            var rnd = new Random();

            string employee_name = employee_names[rnd.Next(0, employee_names.Length)];
            string department_name = department_names[rnd.Next(0, department_names.Length)];
            int age = rnd.Next(22, 35);
            double salary = rnd.Next(30000, 100000);

            var http = new HttpClient
            {
                BaseAddress = new Uri(Configuration["WebAPI"])
            };

            //var response = http.PostAsJsonAsync($"api/Departments/add/{department_name}/{employee_name}/{age}/{salary}").Result;
            //if (!response.IsSuccessStatusCode)
            //    MessageBox.Show("Ошибка при добавлении нового сотрудника", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);

            var departments = http.GetFromJsonAsync<Department[]>("api/Departments").Result;
            string res = "";
            foreach(Department dp in departments)
            {
                dp.Employees = http.GetFromJsonAsync<Employee[]>($"api/Departments/Department/{dp.Id}").Result;
                res += dp.Id + " " + dp.Name + "\t";
                foreach (Employee emp in dp.Employees)
                    res += emp.Id + " " + emp.Name + " " + emp.Age + " " + emp.Salary + "\n";
            }
            //Application.Run(new Form1());
        }
    }
}
