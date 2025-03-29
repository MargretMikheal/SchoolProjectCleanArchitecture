using MediatR;
using SchoolProject.Core.Bases;

namespace SchoolProject.Core.Features.Authorization.Commands.Models
{
    public class EditRoleCommand : IRequest<Response<string>>
    {
        public string RoleId { get; set; }
        public string RoleName { get; set; }
    }
}
