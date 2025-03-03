using MediatR;
using SchoolProject.Core.Bases;
using SchoolProject.Core.Features.User.Query.Results;

namespace SchoolProject.Core.Features.User.Query.Models
{
    public class GetUserByIdQuery : IRequest<Response<GetUserByIdResponse>>
    {
        public GetUserByIdQuery(string id)
        {
            Id = id;
        }
        public GetUserByIdQuery() { }

        public string Id { get; set; }
    }
}
