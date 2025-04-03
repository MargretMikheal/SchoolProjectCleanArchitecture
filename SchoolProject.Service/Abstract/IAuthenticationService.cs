using SchoolProject.Data.Entities.Identity;
using SchoolProject.Data.Helper.Dtos;
using System.IdentityModel.Tokens.Jwt;

namespace SchoolProject.Service.Abstract
{
    public interface IAuthenticationService
    {
        public Task<JwtAuthResult> GetJWTToken(ApplicationUser user);
        public Task<JwtAuthResult> GetRefreshToken(ApplicationUser user, JwtSecurityToken jwtToken, DateTime? ExpireDate, string refreshToken);
        public JwtSecurityToken ReadJwtToken(string token);
        public Task<(string, DateTime?)> ValidateDetails(JwtSecurityToken jwtToken, string accessToken, string refreshToken);
        public Task<string> ValidateToken(string token);
        public Task<string> ConfirmEmail(string userId, string code);
        public Task<string> SendResetPasswordCode(string Email);
        public Task<string> ConfirmResetPassword(string Email, string code);
        public Task<string> ResetPassword(string Email, string password);
    }
}
