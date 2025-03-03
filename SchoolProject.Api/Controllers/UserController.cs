using Microsoft.AspNetCore.Mvc;
using SchoolProject.Api.ApiBases;
using SchoolProject.Core.Features.User.Command.Models;
using SchoolProject.Core.Features.User.Query.Models;
using SchoolProject.Data.AppMetaData;

namespace SchoolProject.Api.Controllers
{
    // [Route("api/[controller]")]
    [ApiController]
    public class UserController : AppControllerBase
    {
        [HttpPost(Router.UserRouting.Add)]
        public async Task<IActionResult> AddUser([FromBody] AddUserCommand command)
        {
            return NewResult(await _mediator.Send(command));
        }

        [HttpGet(Router.UserRouting.List)]
        public async Task<IActionResult> AllUsers([FromQuery] GetUserPaginatedListQuery GetUserPaginatedListQuery)
        {
            return Ok(await _mediator.Send(GetUserPaginatedListQuery));
        }
        [HttpGet(Router.UserRouting.byId)]
        public async Task<IActionResult> GetUserById([FromRoute] string id)
        {
            var query = new GetUserByIdQuery { Id = id };
            return Ok(await _mediator.Send(query));
        }

    }
}
