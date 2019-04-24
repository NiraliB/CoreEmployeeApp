using EmpAppCoreEF_Self.Data;
using EmpAppCoreEF_Self.Models;
using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace ManageServices
{
    public class EmployeeService
    {
        private readonly EmpAppDbContext _context;

        public EmployeeService(EmpAppDbContext cont)
        {
            _context = cont;
        }

        public List<EmployeeModel> GetEmployeeList()
        {
            return _context.EmployeeModel.Include(e => e.DepartmentModel).ToList();
        }
        public void Dispose()
        {
            throw new NotImplementedException();
        }
    }
}
