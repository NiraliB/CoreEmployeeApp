using EmpAppCoreEF_Self.Data;
using EmpAppCoreEF_Self.Models;
using EmpAppCoreEF_Self.Models.ViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EmpAppCoreEF_Self.Controllers
{
    public class AccountController : Controller
    {
        private readonly EmpAppDbContext _dbContext;
        private readonly UserManager<IdentityUser> _userManage;
        private readonly SignInManager<IdentityUser> _signInManage;

        public AccountController(EmpAppDbContext dbNew, UserManager<IdentityUser> userManage, SignInManager<IdentityUser> signInManage)
        {
            _dbContext = dbNew;
            _userManage = userManage;
            _signInManage = signInManage;
        }

        [HttpGet]
        public IActionResult Login(string returnUrl = "")
        {
            var model = new LoginViewModel { ReturnUrl = returnUrl };
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Login(LoginViewModel objLoginViewModel)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    if (objLoginViewModel != null)
                    {
                        var result = await _signInManage.PasswordSignInAsync(objLoginViewModel.UserName,
                            objLoginViewModel.Password, false, false);

                        if (result.Succeeded)
                        {
                            if (!string.IsNullOrEmpty(objLoginViewModel.ReturnUrl) && Url.IsLocalUrl(objLoginViewModel.ReturnUrl))
                            {
                                return Redirect(objLoginViewModel.ReturnUrl);
                            }
                            else
                            {
                                return RedirectToAction("Index", "Home");
                            }
                        }
                    }
                }
                ModelState.AddModelError("", "Invalid Login");
                return View(objLoginViewModel);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpGet]
        public IActionResult SignUp()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> SignUp(RegisterViewModel objRegister)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    if (objRegister != null)
                    {
                        var user = new IdentityUser { UserName = objRegister.UserName, Email = objRegister.EmailId };
                        var result = await _userManage.CreateAsync(user, objRegister.Password);
                        if (result.Succeeded)
                        {
                            await _signInManage.SignInAsync(user, false);
                            return RedirectToAction("Index", "Home");
                        }
                        else
                        {
                            foreach (var error in result.Errors)
                            {
                                ModelState.AddModelError("", error.Description);
                            }
                        }

                    }
                }
                return View(objRegister);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public IActionResult Logout()
        {
            return RedirectToAction("Login");
        }


    }
}