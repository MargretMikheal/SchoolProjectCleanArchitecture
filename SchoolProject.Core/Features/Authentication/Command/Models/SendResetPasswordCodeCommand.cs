
using MediatR;
using SchoolProject.Core.Bases;

namespace SchoolProject.Core.Features.Authentication.Command.Models
{
    public class SendResetPasswordCodeCommand : IRequest<Response<string>>
    {
        public string Email { get; set; }
    }
}
