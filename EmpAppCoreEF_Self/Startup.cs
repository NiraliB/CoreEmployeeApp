using EmpAppCoreEF_Self.Data;
using EmpAppCoreEF_Self.EmpService;
using EmpAppCoreEF_Self.Models;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Threading.Tasks;

namespace EmpAppCoreEF_Self
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.Configure<CookiePolicyOptions>(options =>
            {
                // This lambda determines whether user consent for non-essential cookies is needed for a given request.
                options.CheckConsentNeeded = context => true;
                options.MinimumSameSitePolicy = SameSiteMode.None;
            });

            services.AddDbContext<EmpAppDbContext>(option =>
                    option.UseSqlServer(Configuration.GetConnectionString("EmpAppDbContext")));

            services.AddIdentity<IdentityUser, IdentityRole>().AddEntityFrameworkStores<EmpAppDbContext>().AddDefaultTokenProviders()
                .AddRoles<IdentityRole>();

            //services.AddDefaultIdentity<IdentityUser>().AddEntityFrameworkStores<EmpAppDbContext>();

            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_1).AddRazorPagesOptions(options =>
            {
                options.AllowAreas = true;
                options.Conventions.AuthorizeAreaFolder("Identity", "/Account/Manage");
                options.Conventions.AuthorizeAreaPage("Identity", "/Account/Logout");
            });

            services.Configure<IdentityOptions>(options =>
            {
                // Password settings.
                options.Password.RequireDigit = true;
                options.Password.RequireLowercase = true;
                options.Password.RequireNonAlphanumeric = true;
                options.Password.RequireUppercase = true;
                options.Password.RequiredLength = 6;
                options.Password.RequiredUniqueChars = 1;

                // Lockout settings.
                //options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(5);
                options.Lockout.MaxFailedAccessAttempts = 5;
                options.Lockout.AllowedForNewUsers = true;

                // User settings.
                options.User.AllowedUserNameCharacters =
                "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789-._@+";
                options.User.RequireUniqueEmail = false;
            });

            services.ConfigureApplicationCookie(options =>
            {
                // Cookie settings
                options.Cookie.HttpOnly = true;
                //options.ExpireTimeSpan = TimeSpan.FromMinutes(5);

                options.LoginPath = "/Identity/Account/Login";
                options.AccessDeniedPath = "/Identity/Account/AccessDenied";
                //options.SlidingExpiration = true;
            });

        }
        
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, IServiceProvider serviceProvider)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseCookiePolicy();
            app.UseAuthentication();
            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller=Account}/{action=Login}/{id?}");
            });

            CreateUserRoles(serviceProvider).Wait();
        }

        private async Task CreateUserRoles(IServiceProvider serviceProvider)
        {
            var roleManager = serviceProvider.GetRequiredService<RoleManager<IdentityRole>>();
            var userManager = serviceProvider.GetRequiredService<UserManager<IdentityUser>>();

            IdentityResult roleResult;
            string[] roleNames = { "Admin", "DeptManager", "EmployeeMember" };
            foreach (var role in roleNames)
            {
                var roleCheck = await roleManager.RoleExistsAsync(role);

                if (!roleCheck)
                {
                    roleResult = await roleManager.CreateAsync(new IdentityRole(role));
                }
            }
            
            var User1 = await userManager.FindByIdAsync("27676498-df68-4042-9b97-cf411912cfb6");  //Pooja
            await userManager.AddToRoleAsync(User1, "EmployeeMember");

            var User2 = await userManager.FindByIdAsync("1e21f43f-7ee7-4093-98a9-287337d0890a"); // Piyush
            await userManager.AddToRoleAsync(User2, "DeptManager");

            var User3 = await userManager.FindByIdAsync("960b963c-96bc-429d-afe4-62e2164e0bd0"); // Nirali
            await userManager.AddToRoleAsync(User3, "Admin");

            var User4 = await userManager.FindByIdAsync("e3b99a5e-13dc-48c2-a0cd-d314d074c7fb"); // Rahil
            await userManager.AddToRoleAsync(User4, "EmployeeMember");

            //

            //var checkResult = await userManager.FindByEmailAsync("ns@gmail.com");
            //if (userid != null)
            //{

            //    var getData = await userManager.AddToRoleAsync(user, "Admin");
            //}

            //var powerUser = new ApplicationUser
            //{
            //    UserName = Configuration.GetSection("UserSettings")["Email"],
            //    Email = Configuration.GetSection("UserSettings")["Email"]
            //};

            //string UserPassword = Configuration.GetSection("UserSettings")["PasswordHash"];

            //var newUser = await userManager.FindByEmailAsync(Configuration.GetSection("UserSettings")["Email"]);
            //if (newUser == null)
            //{
            //    var createPowerUser = await userManager.CreateAsync(powerUser, UserPassword);
            //    if (createPowerUser.Succeeded)
            //    {
            //        await userManager.AddToRoleAsync(powerUser, "Admin");
            //    }
            //}

        }
    }
}
