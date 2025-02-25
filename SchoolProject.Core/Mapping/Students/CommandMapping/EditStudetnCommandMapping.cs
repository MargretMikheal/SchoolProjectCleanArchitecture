using SchoolProject.Core.Features.Students.Command.Models;
using SchoolProject.Data.Entities;

namespace SchoolProject.Core.Mapping.Students
{
    public partial class StudentProfile
    {
        public void EditStudetnCommandMapping()
        {
            CreateMap<EditStudentCommand, Student>()
                   .ForMember(dest => dest.DID, opt => opt.MapFrom(src => src.DepartmentID))
                     .ForMember(dest => dest.StudID, opt => opt.MapFrom(src => src.Id));
        }
    }
}
