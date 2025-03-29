using MediatR;
using SchoolProject.Core.Bases;
using SchoolProject.Data.Helper.Dtos;

namespace SchoolProject.Core.Features.Authorization.Commands.Models
{
    public class UpdateUserClaimsCommand : ManageUserClaims, IRequest<Response<string>>
    {
    }
}
