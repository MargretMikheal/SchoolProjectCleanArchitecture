using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Localization;
using SchoolProject.Api.Resources;
using SchoolProject.Core.Bases;
using SchoolProject.Core.Features.Authentication.Query.Models;
using SchoolProject.Data.Entities.Identity;
using SchoolProject.Service.Abstract;

namespace SchoolProject.Core.Features.Authentication.Query.Handler
{
    public class AuthenticationQueryHandler : ResponseHandler,
        IRequestHandler<AuthorizeUserQuery, Response<string>>,
        IRequestHandler<ConfirmResetPasswordQuery, Response<string>>,
        IRequestHandler<ConfirmEmailQuery, Response<string>>
    {
        #region Fields
        private readonly IStringLocalizer _stringLocalizer;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IAuthenticationService _authenticationService;
        #endregion

        #region Ctor
        public AuthenticationQueryHandler(IStringLocalizer<SharedResources> localizer, UserManager<ApplicationUser> userManager, IAuthenticationService authenticationService) : base(localizer)
        {
            _stringLocalizer = localizer;
            _userManager = userManager;
            _authenticationService = authenticationService;
        }
        #endregion
        #region Methods

        public async Task<Response<string>> Handle(AuthorizeUserQuery request, CancellationToken cancellationToken)
        {
            var result = await _authenticationService.ValidateToken(request.Accesstoken);
            if (result == "NotExpired")
                return Success(result);
            return Unauthorized<string>(_stringLocalizer[SharedResourcesKeys.InvalidToken]);
        }

        public async Task<Response<string>> Handle(ConfirmEmailQuery request, CancellationToken cancellationToken)
        {
            var confirmEmailResult = await _authenticationService.ConfirmEmail(request.userId, request.code);
            switch (confirmEmailResult)
            {
                case "UserNotFound":
                    return Unauthorized<string>(_stringLocalizer[SharedResourcesKeys.UserIsNotFound]);
                case "FailedToConfirmEmail":
                    return BadRequest<string>(_stringLocalizer[SharedResourcesKeys.FailedToConfirmEmail]);

            }
            return Success(confirmEmailResult);
        }

        public async Task<Response<string>> Handle(ConfirmResetPasswordQuery request, CancellationToken cancellationToken)
        {
            var resetPasswordResult = await _authenticationService.ConfirmResetPassword(request.Email, request.Code);

            if (resetPasswordResult == "CodeIsNotValid")
                return BadRequest<string>(_stringLocalizer[SharedResourcesKeys.CodeIsNotValid]);

            return Success(resetPasswordResult);
        }
        #endregion
    }
}
