using EmpAppCoreEF_Self.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace ManageServices
{
    public interface IEmpService
    {
        List<EmployeeModel> GetEmployeeList();
    }
}
