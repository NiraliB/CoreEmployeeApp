using EmpAppCoreEF_Self.Controllers;
using EmpAppCoreEF_Self.Data;
using EmpAppCoreEF_Self.EmpService;
using EmpAppCoreEF_Self.Models;
using System;
using System.Threading.Tasks;
using Xunit;

namespace XUnitTestOnEmployee
{
    public class EmployeeTestCase
    {
        public EmpAppDbContext _context;

        public EmployeeTestCase(EmpAppDbContext newEmpDb)
        {
            _context = newEmpDb;
        }


        [Fact]
        public void TestForGetEmployeeAsync()
        {
            EmployeeService objService = new EmployeeService(_context);
            var tes = objService.GetEmployeeList();
            Assert.IsType<EmployeeModel>(tes);
        }

        [Fact]
        public void Task_Add_TwoNumber()
        {
            // Arrange  
            double num1 = 5;
            double num2 = 6;
            var expectedValue = 11;

            // Act  
            var sum = (num1 + num2);

            //Assert  
            Assert.Equal(expectedValue, sum, 1);
        }
    }
}
