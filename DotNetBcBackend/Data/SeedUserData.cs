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


            var sampleMember = new ApplicationUser
            {
                LockoutEnabled = true,
                FirstName = "Jennifer",
                LastName = "Johnson",
                City = "Burnaby",
                Email = "Bob_Johnson@bcit.ca",
                NormalizedEmail = "BOB_JOHNSON@BCIT.CA",
                UserName = "jjohnson",
                NormalizedUserName = "JJOHNSON",
                IsActive = true,
                NotifyJobs = true,
                Created = DateTime.Now,
                SecurityStamp = Guid.NewGuid().ToString()
            };

            //Hash the passwords
            if (!db.Users.Any(u => u.UserName == sampleMember.UserName))
            {
                var password = new PasswordHasher<ApplicationUser>();
                var hashed = password.HashPassword(sampleMember, "P@$$w0rd");
                sampleMember.PasswordHash = hashed;

                var userStore = new UserStore<ApplicationUser>(db);
                await userStore.CreateAsync(sampleMember);
                await userStore.AddToRoleAsync(sampleMember, "MEMBER");
            }
            
            await db.SaveChangesAsync();
        }

    }
}
