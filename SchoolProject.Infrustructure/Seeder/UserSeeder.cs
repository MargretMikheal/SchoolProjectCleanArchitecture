using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using SchoolProject.Data.Entities.Identity;
using SchoolProject.Data.Helper.Enums;

namespace SchoolProject.Infrastructure.Seeder
{
    public static class UserSeeder
    {
        public static async Task SeedAsync(UserManager<ApplicationUser> _userManager, RoleManager<IdentityRole> _roleManager)
        {
            if (!await _roleManager.RoleExistsAsync(RoleType.Admin.ToString()))
            {
                await _roleManager.CreateAsync(new IdentityRole(RoleType.Admin.ToString()));
            }
            if (await _userManager.Users.AnyAsync())
            {
                return;
            }

            var user = new ApplicationUser()
            {
                UserName = "admin",
                Email = "admin@school.com",
                FirstName = "Admin",
                LastName = "User",
                Address = "123 Admin St",
                Country = "USA",
                EmailConfirmed = true
            };
            await _userManager.CreateAsync(user, "P@ssw0rd!");
            await _userManager.AddToRoleAsync(user, RoleType.Admin.ToString());
            await _userManager.AddToRoleAsync(user, RoleType.User.ToString());

        }
    }
}
