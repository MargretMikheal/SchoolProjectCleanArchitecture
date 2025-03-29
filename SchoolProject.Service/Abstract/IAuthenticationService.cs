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
    }
}
