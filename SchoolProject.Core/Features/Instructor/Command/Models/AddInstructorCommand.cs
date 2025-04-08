using MediatR;
using SchoolProject.Core.Bases;

namespace SchoolProject.Core.Features.Instructor.Command.Models
{
    public class AddInstructorCommand : IRequest<Response<string>>
    {
        public string? NameAr { get; set; }
        public string? NameEn { get; set; }
        public string? Position { get; set; }
        public string? Address { get; set; }
        public decimal? Salary { get; set; }
        public int? SupervisorId { get; set; }
        public int? DID { get; set; }
    }
}

