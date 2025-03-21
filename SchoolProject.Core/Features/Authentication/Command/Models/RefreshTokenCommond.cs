using MediatR;
using SchoolProject.Core.Bases;
using SchoolProject.Data.Helper;

namespace SchoolProject.Core.Features.Authentication.Command.Models
{
    public class RefreshTokenCommond : IRequest<Response<JwtAuthResult>>
    {
        public string Token { get; set; }
        public string RefreshToken { get; set; }
    }
}
