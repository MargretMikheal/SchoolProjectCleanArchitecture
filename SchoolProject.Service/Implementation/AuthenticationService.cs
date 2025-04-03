using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using SchoolProject.Data.Entities.Identity;
using SchoolProject.Data.Helper;
using SchoolProject.Data.Helper.Dtos;
using SchoolProject.Infrastructure.Abstract;
using SchoolProject.Infrastructure.Data;
using SchoolProject.Service.Abstract;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;

namespace SchoolProject.Service.Implementation
{
    public class AuthenticationService : IAuthenticationService
    {
        #region Fields
        private readonly JWT _jwt;
        private readonly IRefreshTokenRepository _refreshTokenRepository;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IEmailService _emailService;
        private readonly ApplicationDbContext _context;
        #endregion

        #region Ctor
        public AuthenticationService(JWT jwt, IRefreshTokenRepository refreshTokenRepository, UserManager<ApplicationUser> userManager, IEmailService emailService, ApplicationDbContext context)
        {
            _jwt = jwt;
            _refreshTokenRepository = refreshTokenRepository;
            _userManager = userManager;
            _emailService = emailService;
            _context = context;
        }
        #endregion


        #region PublicMethods
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

        public async Task<string> ConfirmEmail(string userId, string code)
        {
            var user = await _userManager.FindByIdAsync(userId);
            if (user == null)
                return "UserNotFound";
            var result = await _userManager.ConfirmEmailAsync(user, code);
            if (!result.Succeeded)
                return "FailedToConfirmEmail";
            return "Success";
        }
        public async Task<string> SendResetPasswordCode(string Email)
        {
            var trans = await _context.Database.BeginTransactionAsync();
            try
            {
                var user = await _userManager.FindByEmailAsync(Email);
                var generator = new Random();
                string randomNumber = generator.Next(0, 1000000).ToString("D6");
                user.Code = randomNumber;
                var UpdateResult = await _userManager.UpdateAsync(user);
                if (!UpdateResult.Succeeded)
                    return "ErrorInUpdateUser";
                var result = await _emailService.SendEmailAsync(Email, "Reset Password", $"Your Reset Password Code is {randomNumber}");
                await trans.CommitAsync();
                return "Success";
            }
            catch (Exception ex)
            {
                await trans.RollbackAsync();
                return "FailedToSendCode";
            }
        }

        public async Task<string> ConfirmResetPassword(string Email, string code)
        {
            var user = await _userManager.FindByEmailAsync(Email);
            if (user.Code == code) return "Success";
            return "CodeIsNotValid";
        }

        public async Task<string> ResetPassword(string Email, string password)
        {
            var trans = await _context.Database.BeginTransactionAsync();
            try
            {
                var user = await _userManager.FindByEmailAsync(Email);
                var result = await _userManager.RemovePasswordAsync(user);
                if (!result.Succeeded)
                    return "ErrorInUpdateUser";
                result = await _userManager.AddPasswordAsync(user, password);
                if (!result.Succeeded)
                    return "ErrorInUpdateUser";
                user.Code = null;
                result = await _userManager.UpdateAsync(user);
                if (!result.Succeeded)
                    return "ErrorInUpdateUser";
                return "Success";
            }
            catch (Exception ex)
            {
                await trans.RollbackAsync();
                return "FailedToResetPassword";
            }
        }
        #endregion
        #region PrivateMethods
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
            List<string> roles = _userManager.GetRolesAsync(user).Result.ToList();
            var userClaims = _userManager.GetClaimsAsync(user).Result;

            var claims = new List<Claim>();

            if (!string.IsNullOrEmpty(user.UserName))
                claims.Add(new Claim(nameof(UserClaimModel.UserName), user.UserName));
            if (!string.IsNullOrEmpty(user.Email))
                claims.Add(new Claim(nameof(UserClaimModel.Email), user.Email));
            if (!string.IsNullOrEmpty(user.Id))
                claims.Add(new Claim(nameof(UserClaimModel.UserId), user.Id));
            if (!string.IsNullOrEmpty(user.PhoneNumber))
                claims.Add(new Claim(nameof(UserClaimModel.PhoneNumber), user.PhoneNumber));

            claims.Add(new Claim(ClaimTypes.NameIdentifier, user.UserName));

            foreach (var role in roles)
            {
                claims.Add(new Claim(ClaimTypes.Role, role));
            }
            claims.AddRange(userClaims);

            return claims;
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




        #endregion
    }

}
