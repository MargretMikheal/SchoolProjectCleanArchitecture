using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Localization;
using SchoolProject.Api.Resources;
using SchoolProject.Core.Bases;
using SchoolProject.Core.Features.User.Query.Models;
using SchoolProject.Core.Features.User.Query.Results;
using SchoolProject.Core.Wrappers;
using SchoolProject.Data.Entities.Identity;

namespace SchoolProject.Core.Features.User.Query.Handler
{
    public class UserQueryHandler : ResponseHandler,
        IRequestHandler<GetUserPaginatedListQuery, PaginatedResult<GetUserPaginatedListResponse>>,
        IRequestHandler<GetUserByIdQuery, Response<GetUserByIdResponse>>
    {
        #region Fields
        private readonly IMapper _mapper;
        private readonly IStringLocalizer<SharedResources> _localizer;
        private readonly UserManager<ApplicationUser> _userManager;
        #endregion
        #region Constructor
        public UserQueryHandler(IMapper mapper, IStringLocalizer<SharedResources> localizer, UserManager<ApplicationUser> userManager) : base(localizer)
        {
            _mapper = mapper;
            _localizer = localizer;
            _userManager = userManager;
        }
        #endregion
        #region Methods
        public async Task<PaginatedResult<GetUserPaginatedListResponse>> Handle(GetUserPaginatedListQuery request, CancellationToken cancellationToken)
        {
            var users = _userManager.Users.AsQueryable();
            var PaginatedUsers = await _mapper.ProjectTo<GetUserPaginatedListResponse>(users).ToPaginatedListAsync(request.PageNumber, request.PageSize);
            return PaginatedUsers;
        }

        public async Task<Response<GetUserByIdResponse>> Handle(GetUserByIdQuery request, CancellationToken cancellationToken)
        {
            var user = await _userManager.FindByIdAsync(request.Id);
            if (user == null)
            {
                return NotFound<GetUserByIdResponse>(_localizer[SharedResourcesKeys.NotFound]);
            }
            var response = _mapper.Map<GetUserByIdResponse>(user);
            return Success(response);
        }
        #endregion
    }
}
