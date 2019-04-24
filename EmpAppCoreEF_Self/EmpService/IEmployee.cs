using EmpAppCoreEF_Self.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EmpAppCoreEF_Self.EmpService
{
   public  interface IEmployee : IDisposable
    {
        List<EmployeeModel> GetEmployeeList();
        EmployeeModel GetEmpById(int Id);
        bool DeleteEmployee(int delId);
    }
}
