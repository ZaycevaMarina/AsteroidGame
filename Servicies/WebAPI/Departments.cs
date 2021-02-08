using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebAPI
{
    [Route("api/[controller]")]//api/Employees
    [ApiController]
    public class Departments : ControllerBase
    {
        [HttpGet]
        public IEnumerable<Models.Department> GetAllEmployees()
        {
            return Enumerable.Range(1, 10)
                .Select(i => new Models.Department
                                (i, 
                                $"Отдел {i}",
                                Enumerable.Range(1, 10)
                                          .Select(i => new Models.Employee
                                                    (i, $"Сотрудник {i}", 20 + i, 20000 + i * 1000)
                                                  )
                                  )
                          );
        }
    }
    //public class Employees : ControllerBase
    //{
    //    [HttpGet]
    //    public IEnumerable<Models.Employee> GetAllEmployees()
    //    {
    //        return Enumerable.Range(1, 10)
    //            .Select(i => new Models.Employee
    //                            (i, $"Сотрудник {i}", 20 + i, 20000 + i * 1000, i % 3));
    //    }
    //}
}
