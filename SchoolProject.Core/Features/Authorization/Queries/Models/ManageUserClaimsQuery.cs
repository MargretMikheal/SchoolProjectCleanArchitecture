using MediatR;
using SchoolProject.Core.Bases;
using SchoolProject.Data.Helper.Dtos;

namespace SchoolProject.Core.Features.Authorization.Queries.Models
{
    public class ManageUserClaimsQuery : IRequest<Response<ManageUserClaims>>
    {
        public ManageUserClaimsQuery(string userId)
        {
            UserId = userId;
        }

        public string UserId { get; set; }
    }
}
