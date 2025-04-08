using Microsoft.AspNetCore.Mvc;
using SchoolProject.Api.ApiBases;
using SchoolProject.Core.Features.Departments.Query.Models;
using SchoolProject.Data.AppMetaData;

namespace SchoolProject.Api.Controllers
{
    // [Route("api/[controller]")]
    [ApiController]
    public class DepartmentController : AppControllerBase
    {


        [HttpGet(Router.DepartmentRouting.byId)]
        public async Task<IActionResult> GetDepartmentById([FromRoute] int id)
        {
            return NewResult(await _mediator.Send(new GetDepartmentByIdQuery(id)));
        }
        [HttpGet(Router.DepartmentRouting.GetDepartmentStudentCount)]
        public async Task<IActionResult> GetDepartmentStudentCount()
        {
            return NewResult(await _mediator.Send(new GetDepartmentStudentCountQuery()));
        }

    }
}
