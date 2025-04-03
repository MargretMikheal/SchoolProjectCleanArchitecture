using MediatR;
using SchoolProject.Core.Bases;

namespace SchoolProject.Core.Features.Authentication.Query.Models
{
    public class ConfirmEmailQuery : IRequest<Response<string>>
    {
        public string userId { get; set; }
        public string code { get; set; }
    }
}
