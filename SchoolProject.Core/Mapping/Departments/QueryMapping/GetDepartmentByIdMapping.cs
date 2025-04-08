using AutoMapper;
using SchoolProject.Core.Features.Departments.Query.Results;
using SchoolProject.Data.Entities;
namespace SchoolProject.Core.Mapping.Departments
{
    public partial class DepartmentProfile : Profile
    {
        public void GetDepartmentByIdMapping()
        {
            CreateMap<Department, GetDepartmentByIdResponse>()
                 .ForMember(dest => dest.Name, memberOptions => memberOptions.MapFrom(src => src.Localize(src.DNameAr, src.DNameEn)))
                 .ForMember(dest => dest.ManagerName, memberOptions => memberOptions.MapFrom(src => src.InsManager.Localize(src.InsManager.NameAr, src.InsManager.NameEn)))
                 .ForMember(dest => dest.StudentList, memberOptions => memberOptions.MapFrom(src => src.Students))
                 .ForMember(dest => dest.Id, memberOptions => memberOptions.MapFrom(src => src.DID))
                 .ForMember(dest => dest.SubjectList, memberOptions => memberOptions.MapFrom(src => src.DepartmentSubjects))
                 .ForMember(dest => dest.InstructorList, memberOptions => memberOptions.MapFrom(src => src.Instructors));

            CreateMap<DepartmetSubject, SubjectResponse>()
                .ForMember(dest => dest.Id, memberOptions => memberOptions.MapFrom(src => src.SubID))
                .ForMember(dest => dest.SubjectName, memberOptions => memberOptions.MapFrom(src => src.Subjects.Localize(src.Subjects.SubjectNameAr, src.Subjects.SubjectNameEn)));
            CreateMap<Student, StudentResponse>()
                .ForMember(dest => dest.Id, memberOptions => memberOptions.MapFrom(src => src.StudID))
                .ForMember(dest => dest.StudentName, memberOptions => memberOptions.MapFrom(src => src.Localize(src.NameAr, src.NameEn)));
            CreateMap<SchoolProject.Data.Entities.Instructor, InstructorResponse>()
                .ForMember(dest => dest.Id, memberOptions => memberOptions.MapFrom(src => src.InsID))
                .ForMember(dest => dest.InstructorName, memberOptions => memberOptions.MapFrom(src => src.Localize(src.NameAr, src.NameEn)));

        }
    }
}
