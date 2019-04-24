using EmpAppCoreEF_Self.Controllers;
using EmpAppCoreEF_Self.Data;
using EmpAppCoreEF_Self.EmpService;
using EmpAppCoreEF_Self.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using Xunit;

namespace UnitTestOnEmp
{
    public class EmployeeUnitTest : IClassFixture<UnitTestDb>
    {
        private ServiceProvider _serviceProvide;

        public EmployeeUnitTest()
        {
            UnitTestDb testDb = new UnitTestDb();
            _serviceProvide = testDb.ServiceProvider;
        }

        [Fact]
        public void GetEmpList()
        {
            using (var context = _serviceProvide.GetService<IEmployee>())
            {
                var getAllEmp = context.GetEmployeeList();
                Assert.IsType<List<EmployeeModel>>(getAllEmp);
            }
        }

        [Fact]
        public void GetEmpTestById()
        {
            using (var context = _serviceProvide.GetService<IEmployee>())
            {
                int empId = 3;
                var getEmp = context.GetEmpById(empId);
                if (getEmp!=null)
                {
                    Assert.IsType<EmployeeModel>(getEmp);
                }
                else
                {
                    Assert.IsNotType<EmployeeModel>(getEmp);
                }
                
            }
        }

        [Fact]
        public void GetEmpDelete()
        {
            using (var context = _serviceProvide.GetService<IEmployee>())
            {
                int empId = 3;
                bool getEmp = context.DeleteEmployee(empId);
                Assert.IsType<bool>(getEmp);
            }
        }

        [Fact]
        public void NameTest()
        {
            string name = "Nirali";
            Assert.Equal("Nirali", name);
        }

        [Fact]
        public void NameNotEquaTest()
        {
            string name = "Nirali";
            Assert.NotEqual("N", name);
        }
    }
}
