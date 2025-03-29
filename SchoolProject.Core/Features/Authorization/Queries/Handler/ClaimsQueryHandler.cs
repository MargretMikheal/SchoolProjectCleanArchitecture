using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Localization;
using SchoolProject.Api.Resources;
using SchoolProject.Core.Bases;
using SchoolProject.Core.Features.Authorization.Queries.Models;
using SchoolProject.Data.Entities.Identity;
using SchoolProject.Data.Helper.Dtos;
using SchoolProject.Service.Abstract;

namespace SchoolProject.Core.Features.Authorization.Queries.Handler
{
    public class ClaimsQueryHandler : ResponseHandler,
        IRequestHandler<ManageUserClaimsQuery, Response<ManageUserClaims>>
    {
        #region Fields
        private readonly IAuthorizationService _authorizationService;
        private readonly IStringLocalizer<SharedResources> _stringLocalizer;
        private readonly UserManager<ApplicationUser> _userManager;
        #endregion

        #region Ctor
        public ClaimsQueryHandler(IStringLocalizer<SharedResources> localizer, IAuthorizationService authorizationService, UserManager<ApplicationUser> userManager) : base(localizer)
        {
            _authorizationService = authorizationService;
            _stringLocalizer = localizer;
            _userManager = userManager;
        }
        public async Task<Response<ManageUserClaims>> Handle(ManageUserClaimsQuery request, CancellationToken cancellationToken)
        {
            var user = await _userManager.FindByIdAsync(request.UserId);
            if (user == null)
                return NotFound<ManageUserClaims>(_stringLocalizer[SharedResourcesKeys.UserIsNotFound]);
            var result = await _authorizationService.GetUserClaims(user);
            return Success(result);
        }
        #endregion
    }
}
