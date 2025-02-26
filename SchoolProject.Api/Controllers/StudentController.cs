using Microsoft.AspNetCore.Mvc;
using SchoolProject.Api.ApiBases;
using SchoolProject.Core.Features.Students.Command.Models;
using SchoolProject.Core.Features.Students.Queries.Models;
using SchoolProject.Data.AppMetaData;

namespace SchoolProject.Api.Controllers
{
    // [Route("api/[controller]")]
    [ApiController]
    public class StudentController : AppControllerBase
    {

        [HttpGet(Router.StudentRouting.List)]
        public async Task<IActionResult> GetStudentList()
        {
            return NewResult(await _mediator.Send(new GetStudentListQuery()));
        }


        [HttpGet(Router.StudentRouting.byId)]
        public async Task<IActionResult> GetStudentById([FromRoute] int id)
        {
            return NewResult(await _mediator.Send(new GetStudentByIdQuery(id)));
        }

        [HttpGet(Router.StudentRouting.Paginated)]
        public async Task<IActionResult> GetStudentListPaginated([FromQuery] GetStudentPaginatedListQuery query)
        {
            var response = await _mediator.Send(query);
            return Ok(response);
        }

        [HttpPost(Router.StudentRouting.Add)]
        public async Task<IActionResult> AddStudent([FromBody] AddStudentCommand command)
        {
            return NewResult(await _mediator.Send(command));
        }

        [HttpPut(Router.StudentRouting.Edit)]
        public async Task<IActionResult> UpdateStudent([FromBody] EditStudentCommand command)
        {
            return NewResult(await _mediator.Send(command));
        }

        [HttpDelete(Router.StudentRouting.Delete)]
        public async Task<IActionResult> DeleteStudent([FromRoute] int id)
        {
            return NewResult(await _mediator.Send(new DeleteStudentCommand(id)));
        }


    }
}
