using AutoMapper;

namespace SchoolProject.Core.Mapping.Instructor
{
    public partial class InstructorProfile : Profile
    {
        public void AddInstructorMapping()
        {
            CreateMap<Features.Instructor.Command.Models.AddInstructorCommand, Data.Entities.Instructor>()
                .ForMember(dest => dest.NameAr, opt => opt.MapFrom(src => src.NameAr))
                .ForMember(dest => dest.NameEn, opt => opt.MapFrom(src => src.NameEn))
                .ForMember(dest => dest.Position, opt => opt.MapFrom(src => src.Position))
                .ForMember(dest => dest.Address, opt => opt.MapFrom(src => src.Address))
                .ForMember(dest => dest.Salary, opt => opt.MapFrom(src => src.Salary))
                .ForMember(dest => dest.SupervisorId, opt => opt.MapFrom(src => src.SupervisorId))
                .ForMember(dest => dest.DID, opt => opt.MapFrom(src => src.DID));
        }
    }
}
