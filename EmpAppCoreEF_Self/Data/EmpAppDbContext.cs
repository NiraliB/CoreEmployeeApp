using EmpAppCoreEF_Self.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EmpAppCoreEF_Self.Data
{
    public class EmpAppDbContext : DbContext
    {
        public EmpAppDbContext(DbContextOptions<EmpAppDbContext> options) : base(options)
        {
        }

        public DbSet<DepartmentModel> DepartmentModel { get; set; }
        public DbSet<EmployeeModel> EmployeeModel { get; set; }
    }
}
