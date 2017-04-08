using DotNetBcBackend.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DotNetBcBackend.Data
{
    public static class SeedUserData
    {
        public static async void Initialize(ApplicationDbContext db, IServiceProvider isp)
        {
            var roleStore = new RoleStore<IdentityRole>(db);

            if (!db.Roles.Any(r => r.Name == "Admin"))
            {
                await roleStore.CreateAsync(new IdentityRole
                {
                    Name = "Admin",
                    NormalizedName = "ADMIN"
                });
            }

            if (!db.Roles.Any(r => r.Name == "Member"))
            {
                await roleStore.CreateAsync(new IdentityRole
                {
                    Name = "Member",
                    NormalizedName = "MEMBER"
                });
            }


            var admin = new ApplicationUser
            {
                LockoutEnabled = true,
                FirstName = "Medhat",
                LastName = "Elmasry",
                City = "Vancouver",
                Email = "Medhat_Elmasry@bcit.ca",
                NormalizedEmail = "MEDHAT_ELMASRY@BCIT.CA",
                UserName = "admin",
                NormalizedUserName = "ADMIN",
                IsActive = true,
                NotifyJobs = true,
                Created = new DateTime(2017, 4, 1),
                SecurityStamp = Guid.NewGuid().ToString()
            };

            //Hash the passwords
            if (!db.Users.Any(u => u.UserName == admin.UserName))
            {
                var password = new PasswordHasher<ApplicationUser>();
                var hashed = password.HashPassword(admin, "P@$$w0rd");
                admin.PasswordHash = hashed;

                var userStore = new UserStore<ApplicationUser>(db);
                await userStore.CreateAsync(admin);
                
            }
			
            //Assign users to roles, attempt 2
			//var userStoreTwo = new UserStore<ApplicationUser>(db);
	        //await userStoreTwo.AddToRoleAsync(admin, "ADMIN");
			
            //Assign users to roles, attempt 1
            //UserManager<ApplicationUser> userManager = isp.GetService<UserManager<ApplicationUser>>();
            //await userManager.AddToRoleAsync(admin, "Admin");
            
            await db.SaveChangesAsync();
        }

    }
}
