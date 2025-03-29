using Microsoft.AspNetCore.Identity;
using SchoolProject.Data.Helper.Enums;

namespace SchoolProject.Infrastructure.Seeder
{
    public static class RoleSeeder
    {
        public static async Task SeedAsync(RoleManager<IdentityRole> _roleManager)
        {
            if (_roleManager.Roles.Any())
            {
                return;
            }

            foreach (var role in Enum.GetValues(typeof(RoleType)))
            {
                string roleName = role.ToString();
                if (!await _roleManager.RoleExistsAsync(roleName))
                {
                    await _roleManager.CreateAsync(new IdentityRole(roleName));
                }
            }
        }
    }
}
