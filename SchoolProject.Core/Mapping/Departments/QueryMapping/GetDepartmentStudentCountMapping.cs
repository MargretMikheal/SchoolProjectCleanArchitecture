using AutoMapper;
using SchoolProject.Core.Features.Departments.Query.Results;
using SchoolProject.Data.Entities.Views;

namespace SchoolProject.Core.Mapping.Departments
{
    public partial class DepartmentProfile : Profile
    {
        public void GetDepartmentStudentCountMapping()
        {
            CreateMap<ViewDepartment, GetDepartmentStudentCountResult>()
                 .ForMember(dest => dest.Name, memberOptions => memberOptions.MapFrom(src => src.Localize(src.DNameAr, src.DNameEn)))
                 .ForMember(dest => dest.StudentCount, memberOptions => memberOptions.MapFrom(src => src.StudCount));
        }
    }
}



