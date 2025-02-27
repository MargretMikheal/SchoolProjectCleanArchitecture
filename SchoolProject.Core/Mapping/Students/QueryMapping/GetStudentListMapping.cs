using SchoolProject.Core.Features.Students.Queries.Results;
using SchoolProject.Data.Entities;

namespace SchoolProject.Core.Mapping.Students
{
    public partial class StudentProfile
    {
        public void GetStudentListMapping()
        {
            CreateMap<Student, GetStudentListResponse>()
                .ForMember(dest => dest.DepartmentName, memberOptions => memberOptions.MapFrom(src => src.Department.DNameAr))
                .ForMember(dest => dest.Name, memberOptions => memberOptions.MapFrom(src => src.Localize(src.NameAr, src.NameEn)));
        }
    }
}
