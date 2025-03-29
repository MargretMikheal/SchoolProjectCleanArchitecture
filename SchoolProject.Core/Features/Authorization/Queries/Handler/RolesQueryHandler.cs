using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Localization;
using SchoolProject.Api.Resources;
using SchoolProject.Core.Bases;
using SchoolProject.Core.Features.Authorization.Queries.Models;
using SchoolProject.Core.Features.Authorization.Queries.Result;
using SchoolProject.Data.Entities.Identity;
using SchoolProject.Data.Helper.Dtos;
using SchoolProject.Service.Abstract;

namespace SchoolProject.Core.Features.Authorization.Queries.Handler
{
    public class RolesQueryHandler : ResponseHandler,
            IRequestHandler<GetRolesListQuery, Response<List<GetRolesResponse>>>,
            IRequestHandler<ManageUserRolesQuery, Response<ManageUserRoles>>,
            IRequestHandler<GetRoleByIdQuery, Response<GetRolesResponse>>
    {
        #region Fields 
        private readonly IAuthorizationService _authorizationService;
        private readonly IMapper _mapper;
        private readonly IStringLocalizer<SharedResources> _Localizer;
        private readonly UserManager<ApplicationUser> _userManager;
        #endregion

        #region Ctor
        public RolesQueryHandler(IStringLocalizer<SharedResources> localizer, IAuthorizationService authorizationService, IMapper mapper, UserManager<ApplicationUser> userManager) : base(localizer)
        {
            _authorizationService = authorizationService;
            _mapper = mapper;
            _Localizer = localizer;
            _userManager = userManager;
        }
        #endregion

        #region Methods
        public async Task<Response<List<GetRolesResponse>>> Handle(GetRolesListQuery request, CancellationToken cancellationToken)
        {
            var roles = await _authorizationService.GetAllRoles();
            var result = _mapper.Map<List<GetRolesResponse>>(roles);
            return Success(result);
        }

        public async Task<Response<GetRolesResponse>> Handle(GetRoleByIdQuery request, CancellationToken cancellationToken)
        {
            var role = await _authorizationService.GetRolesById(request.Id);
            if (role == null)
                return NotFound<GetRolesResponse>(_Localizer[SharedResourcesKeys.NotFound]);
            var result = _mapper.Map<GetRolesResponse>(role);
            return Success(result);
        }

        public async Task<Response<ManageUserRoles>> Handle(ManageUserRolesQuery request, CancellationToken cancellationToken)
        {
            var user = await _userManager.FindByIdAsync(request.UserId);
            if (user == null)
                return NotFound<ManageUserRoles>(_Localizer[SharedResourcesKeys.NotFound]);
            var result = await _authorizationService.GetUserRoles(user);
            return Success(result);
        }
        #endregion
    }
}
