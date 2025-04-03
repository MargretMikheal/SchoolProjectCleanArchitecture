using SchoolProject.Data.Entities.Identity;

namespace SchoolProject.Service.Abstract
{
    public interface IApplicationUserService
    {
        Task<string> AddUserAsync(ApplicationUser user, string password);
        Task<ApplicationUser> FindByEmailAsync(string email);
        Task<ApplicationUser> FindByNameAsync(string userName);
    }

}
