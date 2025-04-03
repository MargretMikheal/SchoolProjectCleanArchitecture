using MediatR;
using SchoolProject.Core.Bases;

namespace SchoolProject.Core.Features.Authentication.Query.Models
{
    public class ConfirmResetPasswordQuery : IRequest<Response<string>>
    {
        public string Email { get; set; }
        public string Code { get; set; }

    }
}
