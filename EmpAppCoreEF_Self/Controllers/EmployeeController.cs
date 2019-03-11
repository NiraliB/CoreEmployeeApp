using EmpAppCoreEF_Self.Data;
using EmpAppCoreEF_Self.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EmpAppCoreEF_Self.Controllers
{
    public class EmployeeController : Controller
    {
        private readonly EmpAppDbContext _context;

        public EmployeeController(EmpAppDbContext Context)
        {
            _context = Context;
        }

        public async Task<IActionResult> EmployeeIndex()
        {
            var employeeList = _context.EmployeeModel.Include(e => e.DepartmentModel);
            return View(await employeeList.ToListAsync());
        }
         
        public async Task<IActionResult> Create(int? id)
        {
            EmployeeModel employee = new EmployeeModel();
            List<SelectListItem> lstDeptData = (from d in _context.DepartmentModel
                                                where (d.IsDelete == false)
                                                select new SelectListItem
                                                {
                                                    Text = d.DeptName,
                                                    Value = d.DeptId.ToString()
                                                }).ToList();

            lstDeptData.Insert(0, new SelectListItem { Text = "Select Department", Value = "" });
            ViewBag.DeptList = lstDeptData;

            if (id != null)
            {
                employee = await _context.EmployeeModel.FindAsync(id);

                if (employee == null)
                {
                    return NotFound();
                }
            }

            return View(employee);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(EmployeeModel objEmpView)
        {
            try
            {
                EmployeeModel objNewEmp = new EmployeeModel();
                if (ModelState.IsValid)
                {
                    objNewEmp.EmpName = objEmpView.EmpName;
                    objNewEmp.EmpSurname = objEmpView.EmpSurname;
                    objNewEmp.Qualification = objEmpView.Qualification;
                    objNewEmp.DeptId = objEmpView.DeptId;
                    objNewEmp.ContactNumber = objEmpView.ContactNumber;
                    objNewEmp.Address = objEmpView.Address;

                    if (objEmpView.EmpId == 0)
                    {
                        _context.Entry(objNewEmp).State = EntityState.Added;
                    }
                    else
                    {
                        objNewEmp.EmpId = objEmpView.EmpId;
                        _context.Entry(objNewEmp).State = EntityState.Modified;
                        
                    }
                    await _context.SaveChangesAsync();
                    return RedirectToAction(nameof(EmployeeIndex));
                }
                return View(objNewEmp);

            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

        public async Task<IActionResult> Delete(int? id)
        {
            try
            {
                EmployeeModel objDelEmp = new EmployeeModel();
                if (id != null)
                {
                    objDelEmp = _context.EmployeeModel.Where(x => x.EmpId == id).FirstOrDefault();

                    if (objDelEmp != null)
                    {
                        _context.Entry(objDelEmp).State = EntityState.Deleted;
                        await _context.SaveChangesAsync();
                    }
                }
                return RedirectToAction(nameof(EmployeeIndex));
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

    }
}