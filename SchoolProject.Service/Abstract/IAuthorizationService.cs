using Microsoft.AspNetCore.Identity;
using SchoolProject.Data.Entities.Identity;
using SchoolProject.Data.Helper.Dtos;

namespace SchoolProject.Service.Abstract
{
    public interface IAuthorizationService
    {
        public Task<string> AddRoleAsync(string roleName);
        public Task<bool> IsRoleExistAsync(string name);
        public Task<string> EditRoleAsync(string id, string name);
        public Task<string> DeleteRoleAsync(string id);
        public Task<string> UpdateUserRoles(ManageUserRoles userRoles);
        public Task<string> UpdateUserClaims(ManageUserClaims request);
        public Task<List<IdentityRole>> GetAllRoles();
        public Task<IdentityRole> GetRolesById(string id);
        public Task<ManageUserRoles> GetUserRoles(ApplicationUser userId);
        public Task<ManageUserClaims> GetUserClaims(ApplicationUser user);
    }
}
