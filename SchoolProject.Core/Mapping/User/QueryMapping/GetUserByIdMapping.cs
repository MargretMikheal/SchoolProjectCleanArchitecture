namespace SchoolProject.Core.Mapping.User
{

    public partial class UserProfile
    {
        public void GetUserByIdMapping()
        {
            CreateMap<Data.Entities.Identity.ApplicationUser, Features.User.Query.Results.GetUserByIdResponse>()
                .ForMember(d => d.Id, opt => opt.MapFrom(s => s.Id))
                .ForMember(d => d.UserName, opt => opt.MapFrom(s => s.UserName))
                .ForMember(d => d.Email, opt => opt.MapFrom(s => s.Email))
                .ForMember(d => d.FullName, opt => opt.MapFrom(s => s.FirstName + " " + s.LastName))
                .ForMember(d => d.PhoneNumber, opt => opt.MapFrom(s => s.PhoneNumber));
        }
    }
}

