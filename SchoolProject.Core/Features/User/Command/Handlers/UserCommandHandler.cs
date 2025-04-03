using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Localization;
using SchoolProject.Api.Resources;
using SchoolProject.Core.Bases;
using SchoolProject.Core.Features.User.Command.Models;
using SchoolProject.Data.Entities.Identity;
using SchoolProject.Service.Abstract;

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
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IEmailService _emailService;
        private readonly IApplicationUserService _appUserService;
        #endregion
        #region Constructor
        public UserCommandHandler(IStringLocalizer<SharedResources> localizer, UserManager<ApplicationUser> userManager, IMapper mapper, IHttpContextAccessor httpContextAccessor, IEmailService emailService, IApplicationUserService appUserService) : base(localizer)
        {
            _localizer = localizer;
            _userManager = userManager;
            _mapper = mapper;
            _httpContextAccessor = httpContextAccessor;
            _emailService = emailService;
            _appUserService = appUserService;
        }
        #endregion
        #region Methods

        public async Task<Response<string>> Handle(AddUserCommand request, CancellationToken cancellationToken)
        {
            //Create User
            var user = _mapper.Map<ApplicationUser>(request);
            var identityResult = await _appUserService.AddUserAsync(user, request.Password);
            switch (identityResult)
            {
                case "ErrorsInCreateUserr":
                    return BadRequest<string>(_localizer[SharedResourcesKeys.FailedToAddUser]);
                case "Failed":
                    return BadRequest<string>(_localizer[SharedResourcesKeys.TryToRegisterAgain]);
                case "Success":
                    return Created<string>(user.Id);
            }
            return BadRequest<string>(identityResult);
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
