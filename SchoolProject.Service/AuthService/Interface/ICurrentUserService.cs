using SchoolProject.Data.Entities.Identity;

namespace SchoolProject.Service.AuthService.Interface
{
    public interface ICurrentUserService
    {
        public Task<ApplicationUser> GetUserAsync();
        public int GetUserId();
        public Task<List<string>> GetCurrentUserRolesAsync();
    }
}
