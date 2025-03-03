using AutoMapper;

namespace SchoolProject.Core.Mapping.User
{
    public partial class UserProfile : Profile
    {
        public UserProfile()
        {
            CreateUserMapping();
            GetUserPaginatedListMapping();
            GetUserByIdMapping();
        }
    }
}
