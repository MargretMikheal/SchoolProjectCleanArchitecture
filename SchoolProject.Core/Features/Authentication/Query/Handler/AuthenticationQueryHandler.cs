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
        IRequestHandler<AuthorizeUserQuery, Response<string>>
    {
        private readonly IStringLocalizer _stringLocalizer;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IAuthenticationService _authenticationService;

        public AuthenticationQueryHandler(IStringLocalizer<SharedResources> localizer, UserManager<ApplicationUser> userManager, IAuthenticationService authenticationService) : base(localizer)
        {
            _stringLocalizer = localizer;
            _userManager = userManager;
            _authenticationService = authenticationService;
        }

        public async Task<Response<string>> Handle(AuthorizeUserQuery request, CancellationToken cancellationToken)
        {
            var result = await _authenticationService.ValidateToken(request.Accesstoken);
            if (result == "NotExpired")
                return Success(result);
            return Unauthorized<string>(_stringLocalizer[SharedResourcesKeys.InvalidToken]);
        }
    }
}
