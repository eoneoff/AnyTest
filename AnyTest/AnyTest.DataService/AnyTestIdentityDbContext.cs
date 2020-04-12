using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AnyTest.ClientAuthentication;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace AnyTest.DataService
{
    /// <summary>
    /// \~english A class that represents a session with identity database
    /// \~ukrainian Клас, який представляє сесію з базою даних Identity
    /// </summary>
    public class AnyTestIdentityDbContext : IdentityDbContext<IdentityUser>
    {
        /// <summary>
    ///     \~english Creates an instance of <c>AnyTestIdentityDbContext</c>
        /// \~ukrainian Створює екземпляр <c>AnyTestIdentityDbContext</c>
        /// </summary>
        /// <param name="options">
        /// \~english Contains the options for database context creation
        /// \~ukrainian Містить опції для створення контексту бази даних
        /// </param>
        /// <returns></returns>
        public AnyTestIdentityDbContext(DbContextOptions<AnyTestIdentityDbContext> options): base(options) { }


        /// <summary>
        /// \~english A method to seed initial data in a newly created database
        /// \~ukrainian Метод, який додає дані за замовчуванням до щойно створеної бази даних
        /// </summary>
        /// <param name="app">
        /// \~english A coller application builder
        /// \~ukrainian Клас-будівник доадтку, викликаючого метод
        /// </param>
        public static async void SeedData(IApplicationBuilder app)
        {
            using var scope = app.ApplicationServices.CreateScope();

            var roleManager = scope.ServiceProvider.GetRequiredService<RoleManager<IdentityRole>>();
            var userManager = scope.ServiceProvider.GetRequiredService<UserManager<IdentityUser>>();

            if(!await roleManager.RoleExistsAsync("Administrator"))
            {
                foreach(var role in AuthService.Roles)
                {
                    await roleManager.CreateAsync(new IdentityRole(role));
                }

                var defaultAdmin = new IdentityUser{UserName = "admin", Email = "admin@admin.com" };
                var createDefaultAdmin = await userManager.CreateAsync(defaultAdmin, "Admin_1");
                if (createDefaultAdmin.Succeeded)
                {
                    await userManager.AddToRoleAsync(defaultAdmin, "Administrator");
                }
            }
        }
    }
}
