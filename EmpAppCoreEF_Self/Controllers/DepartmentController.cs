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

        public IActionResult Index()
        {
            //return View(await _context.DepartmentModel.ToListAsync());
            return View();
        }

        [HttpPost]
        public async Task<JsonResult> SaveData(DepartmentModel objDept)
        {
            string result = string.Empty;
            try
            {
                DepartmentModel objNewDept = new DepartmentModel();

                objNewDept.DeptName = objDept.DeptName;
                objNewDept.IsDelete = false;

                if (objDept.DeptId == 0)
                {
                    _context.Entry(objNewDept).State = EntityState.Added;
                    result = "Saved";
                }
                else
                {
                    objNewDept.DeptId = objDept.DeptId;
                    _context.Entry(objNewDept).State = EntityState.Modified;
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
        public JsonResult GetDepartmentDataLoad()
        {
            List<DepartmentModel> lstDept = new List<DepartmentModel>();
            try
            {
                lstDept = _context.DepartmentModel.Where(x => x.IsDelete == false).OrderByDescending(z => z.DeptId).ToList();
                var finalRes = (from d in lstDept
                                select new[] {
                                    Convert.ToString(d.DeptId),
                                    Convert.ToString(d.DeptName)
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
        public async Task<JsonResult> DeleteDepartment(int id)
        {
            string finalRes = string.Empty;
            try
            {
                if (id != 0)
                {
                    var delDept = _context.DepartmentModel.Where(x => x.DeptId == id).FirstOrDefault();
                    if (delDept != null)
                    {
                        var ifExistEmp = _context.EmployeeModel.Where(e => e.DeptId == delDept.DeptId).ToList();

                        if (ifExistEmp != null && ifExistEmp.Count > 0)
                        {
                            finalRes = "EmployeeIsThere";
                        }
                        else
                        {
                            delDept.IsDelete = true;
                            _context.Entry(delDept).State = EntityState.Modified;
                            await _context.SaveChangesAsync();
                            finalRes = "Deleted";
                        }
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

        [HttpPost]
        public JsonResult EditDepartmentData(int id)
        {
            object editData = new object();
            try
            {
                if (id != 0)
                {
                    editData = _context.DepartmentModel.Where(x => x.DeptId == id).FirstOrDefault();
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


    }
}