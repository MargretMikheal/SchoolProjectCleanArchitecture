using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using SchoolProject.Data.Entities.Identity;
using SchoolProject.Data.Helper;
using SchoolProject.Infrastructure.Abstract;
using SchoolProject.Service.Abstract;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;

namespace SchoolProject.Service.Implementation
{
    public class AuthenticationService : IAuthenticationService
    {
        private readonly JWT _jwt;
        private readonly IRefreshTokenRepository _refreshTokenRepository;
        private readonly UserManager<ApplicationUser> _userManager;

        public AuthenticationService(JWT jwt, IRefreshTokenRepository refreshTokenRepository, UserManager<ApplicationUser> userManager)
        {
            _jwt = jwt;
            _refreshTokenRepository = refreshTokenRepository;
            _userManager = userManager;
        }

        public async Task<JwtAuthResult> GetJWTToken(ApplicationUser user)
        {
            var accessToken = GenerateJwtToken(user);
            var refreshToken = CreateRefreshToken(user.UserName);
            await SaveUserRefreshToken(user, accessToken, refreshToken);

            return new JwtAuthResult { AccessToken = accessToken, refreshToken = refreshToken };
        }

        public async Task<string> ValidateToken(string token)
        {
            var handler = new JwtSecurityTokenHandler();
            var param = GetTokenValidationParameters(token);
            try
            {
                var validator = handler.ValidateToken(token, param, out _);
                if (validator == null)
                    return "InvalidToken";
                return "NotExpired";
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }

        public async Task<JwtAuthResult> GetRefreshToken(ApplicationUser user, JwtSecurityToken jwtToken, DateTime? ExpireDate, string refreshToken)
        {
            var newAccessToken = GenerateJwtToken(user);
            var newRefreshToken = new RefreshToken
            {
                UserName = GetClaimValue(jwtToken, nameof(UserClaimModel.UserName)),
                RefreshTokenString = refreshToken,
                ExpireAt = (DateTime)ExpireDate
            };

            return new JwtAuthResult { AccessToken = newAccessToken, refreshToken = newRefreshToken };
        }

        public JwtSecurityToken ReadJwtToken(string token)
        {
            if (string.IsNullOrEmpty(token)) throw new ArgumentNullException(nameof(token));
            return new JwtSecurityTokenHandler().ReadJwtToken(token);
        }
        public async Task<(string, DateTime?)> ValidateDetails(JwtSecurityToken jwtToken, string accessToken, string refreshToken)
        {
            if (jwtToken.Header.Alg != SecurityAlgorithms.HmacSha256)
                return ("InvalidAlgorithm", null);
            if (jwtToken.ValidTo > DateTime.UtcNow)
                return ("TokenHasNotExpiredYet", null);

            var userId = GetClaimValue(jwtToken, nameof(UserClaimModel.UserId));

            var userRefreshToken = await GetUserRefreshToken(userId, accessToken, refreshToken);
            if (userRefreshToken == null)
                return ("RefreshTokenIsNotFound", null);

            if (userRefreshToken.ExpirDate < DateTime.UtcNow)
            {
                userRefreshToken.IsRevoked = true;
                userRefreshToken.IsUsed = false;
                _refreshTokenRepository.UpdateAsync(userRefreshToken);
                return ("RefreshTokenExpired", null);
            }
            return (userId, userRefreshToken.ExpirDate);
        }


        private TokenValidationParameters GetTokenValidationParameters(string token)
        {
            return new TokenValidationParameters
            {
                ValidateIssuer = _jwt.validateIssure,
                ValidateAudience = _jwt.validateAudience,
                ValidateIssuerSigningKey = _jwt.validateIssureSignInKey,
                IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwt.Key)),
                ValidIssuer = _jwt.Issuer,
                ValidAudience = _jwt.Audience,
                ValidateLifetime = _jwt.validateLifeTime
            };
        }


        private async Task<UserRefreshToken> GetUserRefreshToken(string userId, string token, string refreshToken)
        {
            return await _refreshTokenRepository.GetTableNoTracking()
                .FirstOrDefaultAsync(x => x.RefreshToken == refreshToken && x.Token == token && x.UserId == userId);
        }


        private string GenerateJwtToken(ApplicationUser user)
        {
            var claims = GetClaims(user);
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwt.Key));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                issuer: _jwt.Issuer,
                audience: _jwt.Audience,
                claims: claims,
                expires: DateTime.UtcNow.AddDays(_jwt.AccessTokenDurationInDays),
                signingCredentials: creds);

            return new JwtSecurityTokenHandler().WriteToken(token);
        }

        private RefreshToken CreateRefreshToken(string userName)
        {
            return new RefreshToken
            {
                ExpireAt = DateTime.UtcNow.AddDays(_jwt.RefreshTokenDurationInDays),
                UserName = userName,
                RefreshTokenString = GenerateRefreshTokenString()
            };
        }

        private async Task SaveUserRefreshToken(ApplicationUser user, string accessToken, RefreshToken refreshToken)
        {
            var userRefreshToken = new UserRefreshToken
            {
                AddedTime = DateTime.UtcNow,
                ExpirDate = DateTime.UtcNow.AddDays(_jwt.RefreshTokenDurationInDays),
                IsUsed = true,
                IsRevoked = false,
                JwtId = Guid.NewGuid().ToString(),
                RefreshToken = refreshToken.RefreshTokenString,
                Token = accessToken,
                UserId = user.Id,
            };
            await _refreshTokenRepository.AddAsync(userRefreshToken);
        }

        private List<Claim> GetClaims(ApplicationUser user)
        {
            return new List<Claim>
            {
                new Claim(nameof(UserClaimModel.UserName), user.UserName),
                new Claim(nameof(UserClaimModel.Email), user.Email),
                new Claim(nameof(UserClaimModel.UserId), user.Id),
                new Claim(nameof(UserClaimModel.PhoneNumber), user.PhoneNumber)
            };
        }

        private string GenerateRefreshTokenString()
        {
            var randomNumber = new byte[32];
            using (var rng = RandomNumberGenerator.Create())
            {
                rng.GetBytes(randomNumber);
            }
            return Convert.ToBase64String(randomNumber);
        }
        private string GetClaimValue(JwtSecurityToken jwtToken, string claimType)
        {
            return jwtToken.Claims.FirstOrDefault(c => c.Type == claimType)?.Value;
        }
    }
}
