﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace AnyTest.DataService
{
    public class AnyTestIdentityDbContext : IdentityDbContext<IdentityUser>
    {
        public AnyTestIdentityDbContext(DbContextOptions<AnyTestIdentityDbContext> options): base(options) { }

        public static async void SeedData(IApplicationBuilder app)
        {
            using var scope = app.ApplicationServices.CreateScope();

            var roleManager = scope.ServiceProvider.GetRequiredService<RoleManager<IdentityRole>>();
            var userManager = scope.ServiceProvider.GetRequiredService<UserManager<IdentityUser>>();

            if(!await roleManager.RoleExistsAsync("Administrator"))
            {
                await roleManager.CreateAsync(new IdentityRole("Administrator"));
                await roleManager.CreateAsync(new IdentityRole("Tutor"));
                await roleManager.CreateAsync(new IdentityRole("Student"));

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
