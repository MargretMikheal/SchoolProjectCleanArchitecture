using MediatR;
using SchoolProject.Core.Features.User.Query.Results;
using SchoolProject.Core.Wrappers;

namespace SchoolProject.Core.Features.User.Query.Models
{
    public class GetUserPaginatedListQuery : IRequest<PaginatedResult<GetUserPaginatedListResponse>>
    {
        public int PageNumber { get; set; }
        public int PageSize { get; set; }
    }
}
