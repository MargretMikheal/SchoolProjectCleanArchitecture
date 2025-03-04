
using AutoMapper;
using SchoolProject.Core.Features.User.Command.Models;
using SchoolProject.Data.Entities.Identity;

namespace SchoolProject.Core.Mapping.User
{
    public partial class UserProfile : Profile
    {
        public void ChangeUserPasswordMapping()
        {
            CreateMap<ChangeUserPasswordCommand, ApplicationUser>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.UserId));
        }
    }
}