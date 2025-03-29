using AutoMapper;
using Microsoft.AspNetCore.Identity;
using SchoolProject.Core.Features.Authorization.Queries.Result;

namespace SchoolProject.Core.Mapping.Authentication
{
    public partial class AuthenticationProfile : Profile
    {
        public void GetRolesListMapping()
        {
            CreateMap<IdentityRole, GetRolesResponse>()
                 .ForMember(dest => dest.Id, memberOptions => memberOptions.MapFrom(src => src.Id))
                 .ForMember(dest => dest.Name, memberOptions => memberOptions.MapFrom(src => src.Name));
        }
    }
}
