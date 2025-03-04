using MediatR;
using SchoolProject.Core.Bases;

namespace SchoolProject.Core.Features.User.Command.Models
{
    public class UpdateUserCommand : IRequest<Response<string>>
    {
        public string UserId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string UserName { get; set; }
        public string Email { get; set; }
        public string? Address { get; set; }
        public string? Country { get; set; }
        public string? Phone { get; set; }
    }
}
