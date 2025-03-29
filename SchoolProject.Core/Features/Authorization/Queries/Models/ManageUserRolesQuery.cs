using MediatR;
using SchoolProject.Core.Bases;
using SchoolProject.Data.Helper.Dtos;

namespace SchoolProject.Core.Features.Authorization.Queries.Models
{
    public class ManageUserRolesQuery : IRequest<Response<ManageUserRoles>>
    {
        public ManageUserRolesQuery(string userId)
        {
            UserId = userId;
        }

        public string UserId { get; set; }
    }
}
