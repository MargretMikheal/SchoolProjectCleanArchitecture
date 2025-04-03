using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Localization;
using SchoolProject.Api.Resources;
using SchoolProject.Core.Bases;
using SchoolProject.Core.Features.Authentication.Command.Models;
using SchoolProject.Data.Entities.Identity;
using SchoolProject.Data.Helper.Dtos;
using SchoolProject.Service.Abstract;

namespace SchoolProject.Core.Features.Authentication.Command.Handler
{
    public class AuthenticationCommandHandler : ResponseHandler,
        IRequestHandler<SignInCommand, Response<JwtAuthResult>>,
        IRequestHandler<SendResetPasswordCodeCommand, Response<string>>,
        IRequestHandler<ResetPasswordCommand, Response<string>>,
        IRequestHandler<RefreshTokenCommond, Response<JwtAuthResult>>
    {
        #region Fields
        private readonly IStringLocalizer _Localizer;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly IAuthenticationService _authenticationService;
        #endregion
        #region Ctor
        public AuthenticationCommandHandler(IStringLocalizer<SharedResources> localizer, UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager, IAuthenticationService authenticationService) : base(localizer)
        {
            _Localizer = localizer;
            _userManager = userManager;
            _signInManager = signInManager;
            _authenticationService = authenticationService;
        }
        #endregion
        #region Methods
        public async Task<Response<JwtAuthResult>> Handle(SignInCommand request, CancellationToken cancellationToken)
        {
            //Check if User is exist or not
            var user = await _userManager.FindByNameAsync(request.UserName);
            if (user == null)
                return BadRequest<JwtAuthResult>(_Localizer[SharedResourcesKeys.NotFound]);
            //Try To sign in
            var signInResult = await _signInManager.CheckPasswordSignInAsync(user, request.Password, false);
            //confimr Email
            if (!user.EmailConfirmed)
                return BadRequest<JwtAuthResult>(_Localizer[SharedResourcesKeys.EmailIsNotConfirmed]);
            //generate Token
            if (!signInResult.Succeeded)
                return BadRequest<JwtAuthResult>(_Localizer[SharedResourcesKeys.BadRequest]);
            //return result
            var result = await _authenticationService.GetJWTToken(user);
            return Success(result);

        }

        public async Task<Response<JwtAuthResult>> Handle(RefreshTokenCommond request, CancellationToken cancellationToken)
        {
            var jwtToken = _authenticationService.ReadJwtToken(request.Token);
            var UserIdAndExpireDate = await _authenticationService.ValidateDetails(jwtToken, request.Token, request.RefreshToken);
            switch (UserIdAndExpireDate.Item1)
            {
                case "InvalidAlgorithm":
                    return Unauthorized<JwtAuthResult>(_Localizer[SharedResourcesKeys.InvalidAlgorithm]);
                case "TokenHasNotExpiredYet":
                    return Unauthorized<JwtAuthResult>(_Localizer[SharedResourcesKeys.TokenHasNotExpiredYet]);
                case "RefreshTokenIsNotFound":
                    return Unauthorized<JwtAuthResult>(_Localizer[SharedResourcesKeys.RefreshTokenIsNotFound]);
                case "RefreshTokenExpired":
                    return Unauthorized<JwtAuthResult>(_Localizer[SharedResourcesKeys.RefreshTokenExpired]);
            }
            var user = await _userManager.FindByIdAsync(UserIdAndExpireDate.Item1);
            if (user == null)
                return NotFound<JwtAuthResult>(_Localizer[SharedResourcesKeys.UserIsNotFound]);

            var result = await _authenticationService.GetRefreshToken(user, jwtToken, UserIdAndExpireDate.Item2, request.RefreshToken);

            return Success(result);
        }

        public async Task<Response<string>> Handle(SendResetPasswordCodeCommand request, CancellationToken cancellationToken)
        {
            var result = await _authenticationService.SendResetPasswordCode(request.Email);
            switch (result)
            {
                case "ErrorInUpdateUser":
                    return BadRequest<string>(_Localizer[SharedResourcesKeys.Error]);
                case "FailedToSendCode":
                    return BadRequest<string>(_Localizer[SharedResourcesKeys.FailedToSendEmail]);

            }
            return Success<string>(_Localizer[SharedResourcesKeys.Success]);
        }

        public async Task<Response<string>> Handle(ResetPasswordCommand request, CancellationToken cancellationToken)
        {
            var result = await _authenticationService.ResetPassword(request.Email, request.Password);
            switch (result)
            {
                case "ErrorInUpdateUser":
                    return BadRequest<string>(_Localizer[SharedResourcesKeys.Error]);
                case "FailedToResetPassword":
                    return BadRequest<string>(_Localizer[SharedResourcesKeys.FailedToResetPassword]);
            }
            return Success<string>(_Localizer[SharedResourcesKeys.Success]);
        }
        #endregion
    }
}
