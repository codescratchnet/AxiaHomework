using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using MovieManager.Models;
using MovieManager.Models.Entities;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Identity;
using MovieManager.Common.Constants;

namespace MovieManager.Data
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            // Customize the ASP.NET Identity model and override the defaults if needed.
            // For example, you can rename the ASP.NET Identity table names and more.
            // Add your customizations after calling base.OnModelCreating(builder);
        }


        #region Seed Data
        public static class UserData
        {
            private static readonly IEnumerable<ApplicationUser> Users = new List<ApplicationUser>()
            {
                new ApplicationUser { UserName = "marvin.perez@axia.life", Email = "marvin.perez@axia.life" },
                new ApplicationUser { UserName = "pakorn@windowslive.com", Email = "pakorn@windowslive.com" },
                new ApplicationUser { UserName = "pakorn_teamleader@windowslive.com", Email = "pakorn_teamleader@windowslive.com" },
                new ApplicationUser { UserName = "pakorn_floorstaff@windowslive.com", Email = "pakorn_floorstaff@windowslive.com" }
            };
            public static async Task SeedUsers(IServiceProvider serviceProvider)
            {
                using (var serviceScope = serviceProvider.GetRequiredService<IServiceScopeFactory>().CreateScope())
                {
                    var dbContext = serviceScope.ServiceProvider.GetService<ApplicationDbContext>();

                    await dbContext.Database.MigrateAsync();

                    var userManager = serviceProvider.GetRequiredService<UserManager<ApplicationUser>>();

                    foreach (var user in Users)
                    {
                        if (await userManager.FindByNameAsync(user.UserName) == null)
                        {
                            var result = await userManager.CreateAsync(user, "AxiaHomework1");

                            if (result.Succeeded)
                            {
                                if(user.UserName == "marvin.perez@axia.life")
                                {
                                    await userManager.AddToRoleAsync(user, UserRoleConst.MANAGER_ROLE);
                                }
                                else if (user.UserName == "pakorn@windowslive.com")
                                {
                                    await userManager.AddToRoleAsync(user, UserRoleConst.ADMIN_ROLE);
                                }         
                                else if (user.UserName == "pakorn_teamleader@windowslive.com")
                                {
                                    await userManager.AddToRoleAsync(user, UserRoleConst.TEAMLEADER_ROLE);
                                }
                                else if (user.UserName == "pakorn_floorstaff@windowslive.com")
                                {
                                    await userManager.AddToRoleAsync(user, UserRoleConst.FLOORSTAFF_ROLE);
                                }
                            }
                        }
                    }
                }
            }
        }

        public static class RolesData
        {
            private static readonly string[] Roles = new string[] { UserRoleConst.ADMIN_ROLE, UserRoleConst.MANAGER_ROLE, UserRoleConst.TEAMLEADER_ROLE, UserRoleConst.FLOORSTAFF_ROLE };
            public static async Task SeedRoles(IServiceProvider serviceProvider)
            {
                using (var serviceScope = serviceProvider.GetRequiredService<IServiceScopeFactory>().CreateScope())
                {
                    var dbContext = serviceScope.ServiceProvider.GetService<ApplicationDbContext>();

                    await dbContext.Database.MigrateAsync();

                    var roleManager = serviceProvider.GetRequiredService<RoleManager<IdentityRole>>();

                    foreach (var role in Roles)
                    {
                        if (!await roleManager.RoleExistsAsync(role))
                        {
                            await roleManager.CreateAsync(new IdentityRole(role));
                        }
                    }
                }
            }
        }
        #endregion

        #region Tables
        public DbSet<Movie> Movies { get; set; }
        #endregion
    }
}
