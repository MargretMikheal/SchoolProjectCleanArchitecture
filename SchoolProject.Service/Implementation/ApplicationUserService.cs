using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using SchoolProject.Data.Entities.Identity;
using SchoolProject.Data.Helper.Enums;
using SchoolProject.Infrastructure.Data;
using SchoolProject.Service.Abstract;

namespace SchoolProject.Service.Implementation
{
    public class ApplicationUserService : IApplicationUserService
    {
        #region Fields
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IEmailService _emailService;
        private readonly ApplicationDbContext _context;
        private readonly IUrlHelper _urlHelper;
        #endregion

        #region Ctor
        public ApplicationUserService(UserManager<ApplicationUser> userManager, IHttpContextAccessor httpContextAccessor, IEmailService emailService, ApplicationDbContext context, IUrlHelper urlHelper)
        {
            _userManager = userManager;
            _httpContextAccessor = httpContextAccessor;
            _emailService = emailService;
            _context = context;
            _urlHelper = urlHelper;
        }
        public async Task<string> AddUserAsync(ApplicationUser user, string password)
        {
            var trans = await _context.Database.BeginTransactionAsync();
            try
            {
                //Create User
                var identityResult = await _userManager.CreateAsync(user, password);
                if (!identityResult.Succeeded)
                {
                    return string.Join(",", identityResult.Errors.Select(x => x.Description).ToList());
                }
                await _userManager.AddToRoleAsync(user, RoleType.User.ToString());

                //send confirm Email
                var code = await _userManager.GenerateEmailConfirmationTokenAsync(user);
                var requestAccessor = _httpContextAccessor.HttpContext.Request;
                var returnUrl = requestAccessor.Scheme + "://" + requestAccessor.Host + _urlHelper.Action("ConfirmEmail", "Authentication", new { userId = user.Id, code = code });
                var message = $"To Confirm Email Click Link: <a href='{returnUrl}'>Link Of Confirmation</a>";
                //mesage or body
                var sendEmailResult = await _emailService.SendEmailAsync(user.Email, message, "Confirm Email");

                await trans.CommitAsync();
                return "Success";
            }
            catch (Exception ex)
            {
                await trans.RollbackAsync();
                return "Failed";
            }
        }
        public async Task<ApplicationUser> FindByEmailAsync(string email)
        {
            return await _userManager.FindByEmailAsync(email);
        }

        public async Task<ApplicationUser> FindByNameAsync(string userName)
        {
            return await _userManager.FindByNameAsync(userName);
        }
        #endregion
    }
}
