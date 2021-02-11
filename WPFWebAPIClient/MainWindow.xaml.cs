using System;
using System.Windows;
using System.Net.Http;
using System.Net.Http.Json;
using Microsoft.Extensions.Configuration;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace WPFWebAPIClient
{
    public partial class MainWindow : Window
    {
        public static IConfiguration Configuration { get; } = new ConfigurationBuilder()
           .AddJsonFile("appsettings.json", false, true)
           .Build();

        private ObservableCollection<Department> _AllDepatments = new ObservableCollection<Department>();
        private ObservableCollection<Employee> _AllEmployees = new ObservableCollection<Employee>();
        public MainWindow()
        {
            InitializeComponent();
        }
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            var http = new HttpClient
            {
                BaseAddress = new Uri(Configuration["WebAPI"])
            };
            var departments = http.GetFromJsonAsync<Department[]>("api/Departments").Result;         
            
            foreach (Department dp in departments)
            {
                dp.Employees = http.GetFromJsonAsync<Employee[]>($"api/Departments/Department/{dp.Id}").Result;
                Department department = new Department();
                department.Id = dp.Id;
                department.Name = dp.Name;
                department.Employees = new List<Employee>();
                foreach (Employee emp in dp.Employees)
                {
                    Employee employee = new Employee();
                    employee.Id = emp.Id;
                    employee.Name = emp.Name;
                    employee.Age = emp.Age;
                    employee.Salary = emp.Salary;
                    department.Employees.Add(employee);
                    _AllEmployees.Add(employee);
                }
                _AllDepatments.Add(department);                
            }
            lbDepartments.ItemsSource = _AllDepatments;
            EmployeeDataGrid.DataContext = _AllEmployees;
        }
        private void lbDepartments_SelectionChanged(object sender, System.Windows.Controls.SelectionChangedEventArgs e)
        {
            ObservableCollection<Employee> employees = new ObservableCollection<Employee>();
            foreach(Department dep in _AllDepatments)
            {
                string s = (lbDepartments.SelectedItem as Department).Name;
                if (dep.Name == (lbDepartments.SelectedItem as Department).Name)
                {
                    foreach (Employee emp in dep.Employees)
                        employees.Add(emp);
                    break;
                }
            }
            EmployeeDataGrid.DataContext = employees;
        }

        private void btAddEmployee_Click(object sender, RoutedEventArgs e)
        {            
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
            var response = http.PostAsJsonAsync($"api/Departments/add/{department_name}/{employee_name}/{age}", salary).Result;
            if (!response.IsSuccessStatusCode)
            {
                //"Ошибка при добавлении нового сотрудника";
                EmployeeDataGrid.DataContext = null;
            }
            else
            {
                //Обновить данные
                EmployeeDataGrid.DataContext = (ICollection< Employee>)http.PostAsJsonAsync("api/Departments/EmployeesByDepartmentName", department_name).Result;
            }
        }
    }
}
