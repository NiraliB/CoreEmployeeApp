using EmpAppCoreEF_Self.Models;
using EmpAppCoreEF_Self.Models.ViewModels;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EmpAppCoreEF_Self.Data
{
    public class EmpAppDbContext : IdentityDbContext
    {
        public EmpAppDbContext(DbContextOptions<EmpAppDbContext> options) : base(options)
        {
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer(@"Server=(localdb)\\mssqllocaldb;Database=EmpAppDbContext;Trusted_Connection=True;MultipleActiveResultSets=true;");
            }
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
        }

        public DbSet<DepartmentModel> DepartmentModel { get; set; }
        public DbSet<EmployeeModel> EmployeeModel { get; set; }
    }
}
