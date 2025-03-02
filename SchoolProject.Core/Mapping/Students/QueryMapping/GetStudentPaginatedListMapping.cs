using SchoolProject.Core.Features.Students.Queries.Results;
using SchoolProject.Data.Entities;
namespace SchoolProject.Core.Mapping.Students
{
    public partial class StudentProfile
    {
        public void GetStudentPaginatedListMapping()
        {
            CreateMap<Student, GetStudentPaginatedListResponse>()
                .ForMember(dest => dest.DepartmentName, memberOptions => memberOptions.MapFrom(src => src.Department.Localize(src.Department.DNameAr, src.Department.DNameEn)))
                .ForMember(dest => dest.Name, memberOptions => memberOptions.MapFrom(src => src.Localize(src.NameAr, src.NameEn)))
                .ForMember(dest => dest.StudId, memberOptions => memberOptions.MapFrom(src => src.StudID))
                .ForMember(dest => dest.Address, memberOptions => memberOptions.MapFrom(src => src.Address));

        }
    }
}


