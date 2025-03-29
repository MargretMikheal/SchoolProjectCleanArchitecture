using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using SchoolProject.Data.Entities.Identity;
using SchoolProject.Data.Helper;
using SchoolProject.Data.Helper.Dtos;
using SchoolProject.Infrastructure.Data;
using SchoolProject.Service.Abstract;
using System.Security.Claims;

namespace SchoolProject.Service.Implementation
{
    public class AuthorizationService : IAuthorizationService
    {
        #region Fields
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly ApplicationDbContext _context;
        #endregion

        #region Ctor
        public AuthorizationService(RoleManager<IdentityRole> roleManager, UserManager<ApplicationUser> userManager, ApplicationDbContext context)
        {
            _roleManager = roleManager;
            _userManager = userManager;
            _context = context;
        }
        #endregion

        #region Methods 
        public async Task<string> AddRoleAsync(string roleName)
        {
            var identityRole = new IdentityRole();
            identityRole.Name = roleName;
            var result = await _roleManager.CreateAsync(identityRole);
            if (result.Succeeded)
                return "Success";
            return "Failed";
        }

        public async Task<string> DeleteRoleAsync(string id)
        {
            var role = await _roleManager.FindByIdAsync(id);
            if (role == null)
                return "NotFound";
            var users = await _userManager.GetUsersInRoleAsync(role.Name);
            if (users.Any())
                return "RoleUsed";
            var result = await _roleManager.DeleteAsync(role);
            if (result.Succeeded)
                return "Success";
            return "Failed";
        }

        public async Task<string> EditRoleAsync(string id, string name)
        {
            var role = await _roleManager.FindByIdAsync(id);
            if (role == null)
                return "NotFound";
            role.Name = name;
            var result = await _roleManager.UpdateAsync(role);
            if (result.Succeeded)
                return "Success";
            return "Failed";
        }

        public async Task<List<IdentityRole>> GetAllRoles()
        {
            var roles = await _roleManager.Roles.ToListAsync();
            return roles;
        }

        public async Task<IdentityRole> GetRolesById(string Id)
        {
            var role = await _roleManager.Roles.FirstOrDefaultAsync(role => role.Id == Id);
            return role;
        }

        public async Task<ManageUserRoles> GetUserRoles(ApplicationUser user)
        {
            var response = new ManageUserRoles();
            var roles = await GetAllRoles();
            //var UserRoles = _userManager.GetRolesAsync(user);
            response.UserId = user.Id;
            var Roles = new List<Roles>();
            foreach (var role in roles)
            {
                var Role = new Roles();
                Role.Id = role.Id;
                Role.Name = role.Name;
                Role.HasRole = await _userManager.IsInRoleAsync(user, role.Name);
                Roles.Add(Role);
            }
            response.Roles = Roles;
            return response;
        }

        public async Task<ManageUserClaims> GetUserClaims(ApplicationUser user)
        {
            var response = new ManageUserClaims();
            response.UserId = user.Id;
            var userClaims = await _userManager.GetClaimsAsync(user);
            var claimsList = new List<Claims>();

            foreach (var claim in ClaimsStore.claims)
            {
                var userClaim = new Claims();
                userClaim.Type = claim.Type;
                userClaim.HasClaim = userClaims.Any(c => c.Type == claim.Type);

                claimsList.Add(userClaim);
            }

            response.Claims = claimsList;
            return response;
        }

        public async Task<bool> IsRoleExistAsync(string name)
        {
            var role = await _roleManager.FindByNameAsync(name);
            if (role == null)
            {
                return false;
            }
            return true;
        }

        public async Task<string> UpdateUserRoles(ManageUserRoles request)
        {
            var transaction = await _context.Database.BeginTransactionAsync();
            try
            {
                var user = await _userManager.FindByIdAsync(request.UserId);
                if (user == null)
                    return "UserNotFound";
                var userRoles = await _userManager.GetRolesAsync(user);
                var removeResult = await _userManager.RemoveFromRolesAsync(user, userRoles);
                if (!removeResult.Succeeded)
                    return "FailedToRemoveOldRoles";
                var selectedRoles = request.Roles.Where(role => role.HasRole).Select(x => x.Name).ToList();
                var result = await _userManager.AddToRolesAsync(user, selectedRoles);
                if (!result.Succeeded)
                    return "FailedToAddNewRoles";
                await transaction.CommitAsync();
                return "Success";
            }
            catch (Exception ex)
            {
                await transaction.RollbackAsync();
                return "FailedToUpdateRoles";
            }
        }

        public async Task<string> UpdateUserClaims(ManageUserClaims request)
        {
            var transaction = await _context.Database.BeginTransactionAsync();
            try
            {
                var user = await _userManager.FindByIdAsync(request.UserId);
                if (user == null)
                    return "UserNotFound";

                var userClaims = await _userManager.GetClaimsAsync(user);
                var removeResult = await _userManager.RemoveClaimsAsync(user, userClaims);
                if (!removeResult.Succeeded)
                    return "FailedToRemoveOldClaims";

                var selectedClaims = request.Claims
                    .Where(role => role.HasClaim)
                    .Select(x => new Claim(type: x.Type, value: x.HasClaim.ToString()))
                    .ToList();

                var result = await _userManager.AddClaimsAsync(user, selectedClaims);
                if (!result.Succeeded)
                    return "FailedToAddNewClaims";
                await transaction.CommitAsync();
                return "Success";
            }
            catch (Exception ex)
            {
                await transaction.RollbackAsync();
                return "FailedToUpdateClaims";
            }
        }
        #endregion

    }
}
