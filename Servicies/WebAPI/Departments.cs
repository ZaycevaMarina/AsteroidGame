using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebAPI
{
    [Route("api/[controller]")]//api/Departments
    [ApiController]
    public class Departments : ControllerBase
    {
        private readonly DepartmentsDB _db;
        private readonly ILogger<Departments> _Logger;

        public Departments(DepartmentsDB db, ILogger<Departments> Logger)
        {
            _db = db;
            _Logger = Logger;
        }
        [HttpGet]// /api/Departments
        public IEnumerable<Models.Department> GetAllDepartments()
        {
            _Logger.LogInformation("Запрос всех отделов");
            return _db.Departments.ToArray();            
        }
        [HttpGet("Employees")]// /api/Departments/Employees
        public IEnumerable<Models.Employee> GetAllEmployees()
        {
            return _db.Employees.ToArray();
        }

        [HttpGet("initialize")] // /api/Departments/initialize
        public void Initialized()
        {
            _Logger.LogInformation("Запрос инициализации тестовых данных");
            if (_db.Departments.Any())
            {
                _Logger.LogInformation("Инициализация тестовых данных не требуется");
                return;
            }
            _db.Departments.AddRange(Enumerable.Range(1, 10)
                .Select(i => new Models.Department
                {
                    //Id = i,
                    Name = $"Отдел {i}",
                    Employees = Enumerable.Range(1, 10).Select(j => new Models.Employee
                    {
                        //Id = i + j - 1,
                        Name = $"Сотрудник {j + (i-1)*10}",
                        Age = 20 + j % 5,
                        Salary = i + j * 10000
                    }).ToArray()
                }));
            _db.SaveChanges();
            _Logger.LogInformation("Тестовые данные добавлены в БД");
        }
        [HttpGet("Department/{id}")] // /api/Departments/Department/5
        public IEnumerable<Models.Employee> GetEmployeesByDepartmentId(int id)
        {
            _Logger.LogInformation($"Запрос данных по отделу id:{id}");

            var department = _db.Departments.Include(d => d.Employees).FirstOrDefault(d => d.Id == id);
            if (department is null)
            {
                _Logger.LogInformation($"Отдел с id:{id} в БД не найден");
                return Enumerable.Empty<Models.Employee>();
            }

            _Logger.LogInformation($"Вывод сотрудников по отделу id:{id}");
            return department.Employees;
        }
        [HttpGet("EmployeesByDepartmentName/{DepartmentName}")] // /api/Departments/EmployeesByDepartmentName/Отдел 11
        public IEnumerable<Models.Employee> GetEmployeesByDepartmentName([FromBody] string department_name)
        {
            _Logger.LogInformation($"Запрос данных по отделу '{department_name}'");

            var department = _db.Departments.Include(d => d.Employees).FirstOrDefault(d => d.Name == department_name);
            if (department is null)
            {
                _Logger.LogInformation($"Отдел '{department_name}' в БД не найден");
                return Enumerable.Empty<Models.Employee>();
            }

            _Logger.LogInformation($"Вывод сотрудников по отделу '{department_name}'");
            return department.Employees;
        }
        [HttpPost("add/{DepartmentName}/{EmployeeName}/{Age}")] // api/Departments/add/Отдел 11/Сотрудник 103/28
        public Models.Employee AddEmployee(string DepartmentName, string EmployeeName, int Age, [FromBody] double Salary)
        {
            _Logger.LogInformation($"Добавление даных для отдела {DepartmentName} по сотруднику {EmployeeName}, возраста {Age} с зарпалатой {Salary}");

            var employee = _db.Employees.FirstOrDefault(emp => emp.Name == EmployeeName && emp.Age == Age && emp.Salary == Salary);
            if (employee is null)
            {
                employee = new Models.Employee
                {
                    Name = EmployeeName,
                    Age = Age,
                    Salary = Salary
                };
                var department = _db.Departments.FirstOrDefault(dep => dep.Name == DepartmentName);
                if (department is null)
                {
                    department = new Models.Department
                    {
                        Name = DepartmentName,
                        Employees = new List<Models.Employee>()
                    };
                    department.Employees.Add(employee);
                    _db.Departments.Add(department);
                }
                _db.Employees.Add(employee);
                _db.SaveChanges();
                _Logger.LogInformation($"Добавлены даные для отдела {DepartmentName} по сотруднику {EmployeeName}, возраста {Age} с зарпалатой {Salary}");
            }
            else
            {
                _Logger.LogInformation($"Cотрудник {EmployeeName}, возраста {Age} с зарпалатой {Salary} уже существует в БД");
            }
            return employee;
        }
    }    
}
