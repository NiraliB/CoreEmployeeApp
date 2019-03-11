using EmpAppCoreEF_Self.Data;
using EmpAppCoreEF_Self.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EmpAppCoreEF_Self.Controllers
{
    public class DepartmentController : Controller
    {
        private readonly EmpAppDbContext _context;

        public DepartmentController(EmpAppDbContext Context)
        {
            _context = Context;
        }

        public async Task<IActionResult> Index()
        {
            return View(await _context.DepartmentModel.Where(x => x.IsDelete == false).ToListAsync());
        }

        public async Task<IActionResult> Create(int? id)
        {
            DepartmentModel department = new DepartmentModel();

            if (id != null)
            {
                department = await _context.DepartmentModel.FindAsync(id);

                if (department == null)
                {
                    return NotFound();
                }
            }

            return View(department);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(DepartmentModel objDeptView)
        {
            try
            {
                DepartmentModel objNewDept = new DepartmentModel();
                if (ModelState.IsValid)
                {
                    objNewDept.DeptName = objDeptView.DeptName;
                    objNewDept.IsDelete = false;

                    if (objDeptView.DeptId == 0)
                    {
                        _context.Add(objNewDept);
                    }
                    else
                    {
                        objNewDept.DeptId = objDeptView.DeptId;
                        _context.Entry(objNewDept).State = EntityState.Modified;
                    }

                    await _context.SaveChangesAsync();
                    return RedirectToAction(nameof(Index));
                }
                return View(objNewDept);

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
                DepartmentModel objDelDept = new DepartmentModel();
                if (id != null)
                {
                    objDelDept = _context.DepartmentModel.Where(x => x.DeptId == id).FirstOrDefault();

                    if (objDelDept != null)
                    {
                        objDelDept.IsDelete = true;
                        _context.Entry(objDelDept).State = EntityState.Modified;
                        await _context.SaveChangesAsync();
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

    }
}