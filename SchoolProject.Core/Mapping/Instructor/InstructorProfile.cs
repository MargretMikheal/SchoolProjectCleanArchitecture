using AutoMapper;

namespace SchoolProject.Core.Mapping.Instructor
{
    public partial class InstructorProfile : Profile
    {
        public InstructorProfile()
        {
            AddInstructorMapping();
        }
    }
}
