using Microsoft.AspNetCore.Mvc;
using SchoolProject.Api.ApiBases;
using SchoolProject.Core.Features.Authorization.Commands.Models;
using SchoolProject.Core.Features.Authorization.Queries.Models;
using SchoolProject.Data.AppMetaData;

namespace SchoolProject.Api.Controllers
{
    //[Route("api/[controller]")]
    [ApiController]
    // [Authorize(Roles = "Admin")]
    public class AuthorizationController : AppControllerBase
    {
        [HttpPost(Router.AuthorizationRouting.AddRole)]
        public async Task<IActionResult> AddRole([FromForm] AddRoleCommand command)
        {
            return NewResult(await _mediator.Send(command));
        }


        [HttpPost(Router.AuthorizationRouting.EditRole)]
        public async Task<IActionResult> EditRole([FromForm] EditRoleCommand command)
        {
            return NewResult(await _mediator.Send(command));
        }


        [HttpPost(Router.AuthorizationRouting.DeleteRole)]
        public async Task<IActionResult> DeleteRole([FromRoute] string Id)
        {
            return NewResult(await _mediator.Send(new DeleteRoleCommand(Id)));
        }


        [HttpPost(Router.AuthorizationRouting.UpdateUserRoles)]
        public async Task<IActionResult> UpdateUserRoles([FromBody] UpdateUserRolesCommand command)
        {
            return NewResult(await _mediator.Send(command));
        }


        [HttpPost(Router.AuthorizationRouting.UpdateUserClaims)]
        public async Task<IActionResult> UpdateUserClaims([FromBody] UpdateUserClaimsCommand command)
        {
            return NewResult(await _mediator.Send(command));
        }


        [HttpGet(Router.AuthorizationRouting.GetAll)]
        public async Task<IActionResult> GetAll()
        {
            return NewResult(await _mediator.Send(new GetRolesListQuery()));
        }


        [HttpGet(Router.AuthorizationRouting.GetById)]
        public async Task<IActionResult> GetById([FromRoute] string Id)
        {
            return NewResult(await _mediator.Send(new GetRoleByIdQuery(Id)));
        }


        [HttpGet(Router.AuthorizationRouting.UserRoles)]
        public async Task<IActionResult> UserRoles([FromRoute] string Id)
        {
            return NewResult(await _mediator.Send(new ManageUserRolesQuery(Id)));
        }


        [HttpGet(Router.AuthorizationRouting.UserClaims)]
        public async Task<IActionResult> UserClaims([FromRoute] string Id)
        {
            return NewResult(await _mediator.Send(new ManageUserClaimsQuery(Id)));
        }

    }
}
