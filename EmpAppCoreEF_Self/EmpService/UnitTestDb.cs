using EmpAppCoreEF_Self.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EmpAppCoreEF_Self.EmpService
{
    public class UnitTestDb
    {
        public ServiceProvider ServiceProvider { get; set; }
        public UnitTestDb()
        {
            var serviceCollection = new ServiceCollection();

            serviceCollection.AddDbContext<EmpAppDbContext>(options => options.UseSqlServer("Server=(localdb)\\mssqllocaldb;Database=EmpAppDbContext;Trusted_Connection=True;MultipleActiveResultSets=true"),
             ServiceLifetime.Transient);
            //serviceCollection.AddScoped<EmployeeService>();
            serviceCollection.AddSingleton<IEmployee, EmployeeService>();
            ServiceProvider = serviceCollection.BuildServiceProvider();
        }
    }
}
