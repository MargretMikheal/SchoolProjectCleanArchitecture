using SchoolProject.Core.Features.Students.Command.Models;
using SchoolProject.Data.Entities;


namespace SchoolProject.Core.Mapping.Students
{
    public partial class StudentProfile
    {
        public void AddStudetnCommandMapping()
        {
            CreateMap<AddStudentCommand, Student>()
                .ForMember(dest => dest.DID, opt => opt.MapFrom(src => src.DepartmentID));
        }
    }
}
