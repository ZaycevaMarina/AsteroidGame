using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace WebAPI
{
    public class DepartmentsDB:DbContext
    {
        public DbSet<Models.Employee> Employees { get; set; }
        public DbSet<Models.Department> Departments { get; set; }
        public DepartmentsDB(DbContextOptions<DepartmentsDB> options):base(options) { }
    }
}
