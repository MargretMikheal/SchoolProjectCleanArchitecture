using AutoMapper;
using SchoolProject.Core.Features.User.Command.Models;
using SchoolProject.Data.Entities.Identity;

namespace SchoolProject.Core.Mapping.User
{
    public partial class UserProfile : Profile
    {
        public void UpdateUserMapping()
        {
            CreateMap<UpdateUserCommand, ApplicationUser>()
               .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.UserId))
               .ForMember(dest => dest.UserName, opt => opt.MapFrom(src => src.UserName))
               .ForMember(dest => dest.Email, opt => opt.MapFrom(src => src.Email))
               .ForMember(dest => dest.PhoneNumber, opt => opt.MapFrom(src => src.Phone))
               .ForMember(dest => dest.FirstName, opt => opt.MapFrom(src => src.FirstName))
               .ForMember(dest => dest.LastName, opt => opt.MapFrom(src => src.LastName))
               .ForMember(dest => dest.EmailConfirmed, opt => opt.MapFrom(src => true))
               .ForMember(dest => dest.PhoneNumberConfirmed, opt => opt.MapFrom(src => true))
               .ForMember(dest => dest.TwoFactorEnabled, opt => opt.MapFrom(src => false))
               .ForMember(dest => dest.LockoutEnabled, opt => opt.MapFrom(src => false))
               .ForMember(dest => dest.AccessFailedCount, opt => opt.MapFrom(src => 0))
               .ForMember(dest => dest.SecurityStamp, opt => opt.MapFrom(src => Guid.NewGuid().ToString()));

        }
    }
}
