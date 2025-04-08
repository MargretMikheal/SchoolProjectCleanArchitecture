using MediatR;
using SchoolProject.Core.Bases;
using SchoolProject.Core.Features.Departments.Query.Results;

namespace SchoolProject.Core.Features.Departments.Query.Models
{
    public class GetDepartmentStudentCountQuery : IRequest<Response<List<GetDepartmentStudentCountResult>>>
    {
    }
}
