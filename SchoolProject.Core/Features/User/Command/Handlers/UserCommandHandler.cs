using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Localization;
using SchoolProject.Api.Resources;
using SchoolProject.Core.Bases;
using SchoolProject.Core.Features.User.Command.Models;
using SchoolProject.Data.Entities.Identity;

namespace SchoolProject.Core.Features.User.Command.Handlers
{
    public class UserCommandHandler : ResponseHandler, IRequestHandler<AddUserCommand, Response<string>>
        , IRequestHandler<UpdateUserCommand, Response<string>>
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
            var foundByEmail = await _userManager.FindByEmailAsync(request.Email);
            if (foundByEmail != null && foundByEmail.Id != request.UserId)
                return BadRequest<string>(_localizer[SharedResourcesKeys.EmailAlreadyTaken]);


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


        #endregion
    }
}
