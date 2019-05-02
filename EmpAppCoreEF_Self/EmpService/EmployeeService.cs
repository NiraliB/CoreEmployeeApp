using EmpAppCoreEF_Self.Data;
using EmpAppCoreEF_Self.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EmpAppCoreEF_Self.EmpService
{
    public class EmployeeService : IEmployee
    {
        private EmpAppDbContext dbContext;

        public EmployeeService(EmpAppDbContext newDb)
        {
          dbContext = newDb;
        }

        public async Task<List<EmployeeModel>> GetEmployeeList()
        {
            return await dbContext.EmployeeModel.Include(e => e.DepartmentModel).ToListAsync();
        }

        public EmployeeModel GetEmpById(int empId)
        {
            try
            {
                var getEmpData = (from emp in dbContext.EmployeeModel
                                  where (emp.EmpId == empId)
                                  select emp).FirstOrDefault();

                return getEmpData;
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public bool DeleteEmployee(int delEmpId)
        {
            try
            {
                EmployeeModel objDelEmp = new EmployeeModel();
                if (delEmpId != 0)
                {
                    objDelEmp = dbContext.EmployeeModel.Where(x => x.EmpId == delEmpId).FirstOrDefault();

                    if (objDelEmp != null)
                    {
                        dbContext.Entry(objDelEmp).State = EntityState.Deleted;
                        dbContext.SaveChanges();
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                }
                else
                {
                    return false;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private bool disposed = false;
        protected virtual void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                if (disposing)
                {
                    dbContext.Dispose();
                }
            }
            this.disposed = true;
        }
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}
