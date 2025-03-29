using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Localization;
using SchoolProject.Api.Resources;
using SchoolProject.Core.Bases;
using SchoolProject.Core.Features.User.Command.Models;
using SchoolProject.Data.Entities.Identity;
using SchoolProject.Data.Helper.Enums;

namespace SchoolProject.Core.Features.User.Command.Handlers
{
    public class UserCommandHandler : ResponseHandler, IRequestHandler<AddUserCommand, Response<string>>
        , IRequestHandler<UpdateUserCommand, Response<string>>
        , IRequestHandler<DeleteUserCommand, Response<string>>
        , IRequestHandler<ChangeUserPasswordCommand, Response<string>>
    {
        #region Fields
        private readonly IStringLocalizer<SharedResources> _localizer;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IMapper _mapper;
        #endregion
        #region Constructor
        public UserCommandHandler(IStringLocalizer<SharedResources> localizer, UserManager<ApplicationUser> userManager, IMapper mapper) : base(localizer)
        {
            _localizer = localizer;
            _userManager = userManager;
            _mapper = mapper;
        }
        #endregion
        #region Methods

        public async Task<Response<string>> Handle(AddUserCommand request, CancellationToken cancellationToken)
        {
            //If Email is already taken
            var result = await _userManager.FindByEmailAsync(request.Email);
            if (result != null)
            {
                return BadRequest<string>(_localizer[SharedResourcesKeys.EmailAlreadyTaken]);
            }
            //If UserName is already taken
            var result2 = await _userManager.FindByNameAsync(request.UserName);
            if (result2 != null)
            {
                return BadRequest<string>(_localizer[SharedResourcesKeys.UserNameAlreadyTaken]);
            }
            //Create User
            var user = _mapper.Map<ApplicationUser>(request);
            var identityResult = await _userManager.CreateAsync(user, request.Password);
            if (identityResult.Succeeded)
            {
                await _userManager.AddToRoleAsync(user, RoleType.User.ToString());
                return Created<string>(user.Id);
            }
            return UnprocessableEntity<string>(identityResult.Errors.FirstOrDefault().Description);
        }

        public async Task<Response<string>> Handle(UpdateUserCommand request, CancellationToken cancellationToken)
        {
            //Check if User exist
            var oldUser = await _userManager.FindByIdAsync(request.UserId);
            //notfound
            if (oldUser == null)
            {
                return NotFound<string>(_localizer[SharedResourcesKeys.NotFound]);
            }

            // Check if new UserName exists with another User
            var foundByUserName = await _userManager.FindByNameAsync(request.UserName);
            if (foundByUserName != null && foundByUserName.Id != request.UserId)
                return BadRequest<string>(_localizer[SharedResourcesKeys.UserNameAlreadyTaken]);

            //mapping
            var udpatedUser = _mapper.Map(request, oldUser);
            //result is success
            var result = await _userManager.UpdateAsync(udpatedUser);
            //message
            if (!result.Succeeded) return BadRequest<string>(_localizer[SharedResourcesKeys.BadRequest]);
            return Success("");
        }

        public async Task<Response<string>> Handle(DeleteUserCommand request, CancellationToken cancellationToken)
        {
            //Check if the Id exist
            var user = await _userManager.FindByIdAsync(request.Id);
            //notfound
            if (user == null)
                return NotFound<string>(_localizer[SharedResourcesKeys.NotFound]);

            var result = await _userManager.DeleteAsync(user);
            if (!result.Succeeded)
                return BadRequest<string>(_localizer[SharedResourcesKeys.BadRequest]);
            return Deleted<string>();
        }

        public async Task<Response<string>> Handle(ChangeUserPasswordCommand request, CancellationToken cancellationToken)
        {
            //get User
            var user = await _userManager.FindByIdAsync(request.UserId);
            //notfound
            if (user == null)
                return NotFound<string>(_localizer[SharedResourcesKeys.NotFound]);

            var result = await _userManager.ChangePasswordAsync(user, request.CurrentPassword, request.NewPassword);
            if (!result.Succeeded)
                return BadRequest<string>(_localizer[SharedResourcesKeys.ChangePassFailed]);
            return Success<string>(_localizer[SharedResourcesKeys.PasswordChanged]);

        }


        #endregion
    }
}
