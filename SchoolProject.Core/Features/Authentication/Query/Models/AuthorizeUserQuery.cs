using MediatR;
using SchoolProject.Core.Bases;

namespace SchoolProject.Core.Features.Authentication.Query.Models
{
    public class AuthorizeUserQuery : IRequest<Response<string>>
    {
        public string Accesstoken { get; set; }
    }
}
