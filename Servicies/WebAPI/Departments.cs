using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
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

        public Departments(DepartmentsDB db)
        {
            _db = db;
        }
        [HttpGet]// /api/Departments
        public IEnumerable<Models.Department> GetAllDepartments()
        {
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
            if (_db.Departments.Any())
                return;
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
        }
        [HttpGet("Department/{id}")] // /api/Departments/5
        public IEnumerable<Models.Employee> GetPlayerGames(int id)
        {
            //_Logger.LogInformation("Запрос данных по игроку id:{0}", id);

            var department = _db.Departments.Include(d => d.Employees).FirstOrDefault(d => d.Id == id);
            if (department is null)
            {
               // _Logger.LogInformation("Игрок с id:{0} в БД не найден", id);
                return Enumerable.Empty<Models.Employee>();
            }

            //foreach (var emp in department.Employees)
            //    emp.IdDep = null;

            //_Logger.LogInformation("Вывод данных по игроку id:{0}", id);
            return department.Employees;
        }
    }    
}
