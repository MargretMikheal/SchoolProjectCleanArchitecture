using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Localization;
using SchoolProject.Api.Resources;
using SchoolProject.Core.Bases;
using SchoolProject.Core.Features.Authorization.Commands.Models;
using SchoolProject.Service.Abstract;

namespace SchoolProject.Core.Features.Authorization.Commands.Handler
{
    public class RoleCommandHandler : ResponseHandler, IRequestHandler<AddRoleCommand, Response<string>>
        , IRequestHandler<EditRoleCommand, Response<string>>
        , IRequestHandler<DeleteRoleCommand, Response<string>>
        , IRequestHandler<UpdateUserRolesCommand, Response<string>>
    {
        #region Fields
        private readonly IStringLocalizer _stringLocalizer;
        private readonly IAuthorizationService _authorizationService;
        #endregion

        #region Ctor
        public RoleCommandHandler(IStringLocalizer<SharedResources> localizer, RoleManager<IdentityRole> roleManager, IAuthorizationService authorizationService) : base(localizer)
        {
            _stringLocalizer = localizer;
            _authorizationService = authorizationService;
        }
        #endregion

        #region Methods 
        public async Task<Response<string>> Handle(AddRoleCommand request, CancellationToken cancellationToken)
        {
            var result = await _authorizationService.AddRoleAsync(request.RoleName);
            if (result == "Success")
                return Success<string>(_stringLocalizer[SharedResourcesKeys.Added]);
            return BadRequest<string>(_stringLocalizer[SharedResourcesKeys.BadRequest]);
        }

        public async Task<Response<string>> Handle(EditRoleCommand request, CancellationToken cancellationToken)
        {
            var result = await _authorizationService.EditRoleAsync(request.RoleId, request.RoleName);
            if (result == "Success")
                return Success<string>(_stringLocalizer[SharedResourcesKeys.Updated]);
            else if (result == "NotFound")
                return NotFound<string>(_stringLocalizer[SharedResourcesKeys.NotFound]);
            return BadRequest<string>(_stringLocalizer[SharedResourcesKeys.BadRequest]);
        }

        public async Task<Response<string>> Handle(DeleteRoleCommand request, CancellationToken cancellationToken)
        {
            var result = await _authorizationService.DeleteRoleAsync(request.Id);
            if (result == "Success")
                return Success<string>(_stringLocalizer[SharedResourcesKeys.Deleted]);
            else if (result == "NotFound")
                return NotFound<string>(_stringLocalizer[SharedResourcesKeys.NotFound]);
            else if (result == "RoleUsed")
                return BadRequest<string>(_stringLocalizer[SharedResourcesKeys.RoleUsed]);
            return BadRequest<string>(_stringLocalizer[SharedResourcesKeys.BadRequest]);
        }

        public async Task<Response<string>> Handle(UpdateUserRolesCommand request, CancellationToken cancellationToken)
        {
            var result = await _authorizationService.UpdateUserRoles(request);
            switch (result)
            {
                case "UserNotFound": return NotFound<string>(_stringLocalizer[SharedResourcesKeys.UserIsNotFound]);
                case "FailedToRemoveOldRoles": return BadRequest<string>(_stringLocalizer[SharedResourcesKeys.RoleUFailedToRemoveOldValues]);
                case "FailedToAddNewRoles": return BadRequest<string>(_stringLocalizer[SharedResourcesKeys.FailedToAddNewValue]);
                case "FailedToUpdateRoles": return BadRequest<string>(_stringLocalizer[SharedResourcesKeys.FailedToUpdate]);
            }
            return Success<string>(_stringLocalizer[SharedResourcesKeys.Success]);
        }
        #endregion
    }
}
