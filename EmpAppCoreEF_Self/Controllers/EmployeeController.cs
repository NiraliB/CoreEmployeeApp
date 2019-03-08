using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EmpAppCoreEF_Self.Data;
using EmpAppCoreEF_Self.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace EmpAppCoreEF_Self.Controllers
{
    public class EmployeeController : Controller
    {
        private readonly EmpAppDbContext _context;

        public EmployeeController(EmpAppDbContext Context)
        {
            _context = Context;
        }

        public IActionResult EmployeeIndex()
        {
            List<DepartmentModel> lstDeptData = _context.DepartmentModel.Where(x=>x.IsDelete == false).ToList();
            SelectList lstDeptSelList = new SelectList(lstDeptData, "DeptId", "DeptName");

            EmployeeModel objEmpModel = new EmployeeModel();
            objEmpModel.DepartmentList = lstDeptSelList;

            return View(objEmpModel);
        }

        [HttpPost]
        public async Task<JsonResult> SaveEmpData(EmployeeModel objViewEmp)
        {
            string result = string.Empty;
            try
            {
                EmployeeModel objNewEmp = new EmployeeModel();
                objNewEmp.EmpName = objViewEmp.EmpName;
                objNewEmp.EmpSurname = objViewEmp.EmpSurname;
                objNewEmp.Qualification = objViewEmp.Qualification;
                objNewEmp.DeptId = objViewEmp.DeptId;
                objNewEmp.ContactNumber = objViewEmp.ContactNumber;
                objNewEmp.Address = objViewEmp.Address;

                if (objViewEmp.EmpId == 0)
                {
                    _context.Entry(objNewEmp).State = EntityState.Added;
                    result = "Saved";
                }
                else {
                    objNewEmp.EmpId = objViewEmp.EmpId;
                    _context.Entry(objNewEmp).State = EntityState.Modified;
                    result = "Updated";
                }

                await _context.SaveChangesAsync();
                return Json(result);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpGet]
        public JsonResult GetEmployeeDataLoad()
        {
            List<EmployeeModel> lstEmployee = new List<EmployeeModel>();
            try
            {
                lstEmployee = _context.EmployeeModel.OrderByDescending(z => z.EmpId).ToList();
                var finalRes = (from d in lstEmployee
                                join dep in _context.DepartmentModel on d.DeptId equals dep.DeptId
                                select new[] {
                                    Convert.ToString(d.EmpId),
                                    Convert.ToString(d.EmpName + ' ' + d.EmpSurname) ,
                                    Convert.ToString(d.Qualification),
                                    Convert.ToString(d.ContactNumber),
                                    Convert.ToString(dep.DeptName)
                                });
                return Json(new
                {
                    aaData = finalRes
                });

            }
            catch (Exception ex)
            {
                return Json(new
                {
                    aaData = new List<string[]> { }
                });

            }
        }

        [HttpPost]
        public JsonResult EditEmployeeDetails(int id)
        {
            object editData = new object();
            try
            {
                if (id != 0)
                {
                    editData = _context.EmployeeModel.Where(x => x.EmpId == id).FirstOrDefault();
                    if (editData != null)
                    {
                        return Json(editData);
                    }
                }
                return Json(editData);
            }
            catch (Exception ex)
            {
                return Json(new
                {
                    aaData = new List<string[]> { }
                });
            }
        }

        [HttpPost]
        public async Task<JsonResult> DeleteEmployee(int id)
        {
            string finalRes = string.Empty;
            try
            {
                if (id != 0)
                {
                    var delEmployee = _context.EmployeeModel.Where(x => x.EmpId == id).FirstOrDefault();
                    if (delEmployee != null)
                    {
                        _context.Entry(delEmployee).State = EntityState.Deleted;
                        await _context.SaveChangesAsync();
                        finalRes = "Deleted";
                    }
                    else
                    {
                        finalRes = "NotDeleted";
                    }
                }
                else
                {

                    finalRes = "There is somthing wrong!";
                }


                return Json(finalRes);
            }
            catch (Exception ex)
            {
                return Json(new
                {
                    aaData = new List<string[]> { }
                });
            }

        }

    }
}