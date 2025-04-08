
using Microsoft.AspNetCore.Mvc;
using SchoolProject.Api.ApiBases;
using SchoolProject.Core.Features.Instructor.Command.Models;
using SchoolProject.Data.AppMetaData;

namespace SchoolProject.Api.Controllers
{
    //[Route("api/[controller]")]
    [ApiController]
    public class InstructorController : AppControllerBase
    {


        [HttpPost(Router.InstructorRouting.Add)]
        public async Task<ActionResult<string>> AddInstructor(AddInstructorCommand command)
        {
            var result = await _mediator.Send(command);
            return Ok(result);
        }
    }
}
