using MediatR;
using SchoolProject.Core.Bases;

namespace SchoolProject.Core.Features.User.Command.Models
{
    public class DeleteUserCommand : IRequest<Response<string>>
    {
        public string Id { get; set; }
        public DeleteUserCommand(string id)
        {
            Id = id;
        }

    }
}
