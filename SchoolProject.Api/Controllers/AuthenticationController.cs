using Microsoft.AspNetCore.Mvc;
using SchoolProject.Api.ApiBases;
using SchoolProject.Core.Features.Authentication.Command.Models;
using SchoolProject.Core.Features.Authentication.Query.Models;
using SchoolProject.Data.AppMetaData;

namespace SchoolProject.Api.Controllers
{
    [ApiController]
    public class AuthenticationController : AppControllerBase
    {
        [HttpPost(Router.AuthenticationRouting.SignIn)]
        public async Task<IActionResult> AddUser([FromForm] SignInCommand command)
        {
            return NewResult(await _mediator.Send(command));
        }

        [HttpPost(Router.AuthenticationRouting.RefreshToken)]
        public async Task<IActionResult> RefreshToken([FromForm] RefreshTokenCommond command)
        {
            return NewResult(await _mediator.Send(command));
        }

        [HttpPost(Router.AuthenticationRouting.SendResetPasswordCodeEmail)]
        public async Task<IActionResult> SendResetPasswordCodeEmail([FromQuery] SendResetPasswordCodeCommand command)
        {
            return NewResult(await _mediator.Send(command));
        }
        [HttpPost(Router.AuthenticationRouting.ResetPassword)]
        public async Task<IActionResult> ResetPassword([FromForm] ResetPasswordCommand command)
        {
            return NewResult(await _mediator.Send(command));
        }

        [HttpGet(Router.AuthenticationRouting.ValidateToken)]
        public async Task<IActionResult> ValidateToken([FromQuery] AuthorizeUserQuery command)
        {
            return NewResult(await _mediator.Send(command));
        }

        [HttpGet(Router.AuthenticationRouting.ConfirmEmail)]
        public async Task<IActionResult> ConfirmEmail([FromQuery] ConfirmEmailQuery command)
        {
            return NewResult(await _mediator.Send(command));
        }

        [HttpGet(Router.AuthenticationRouting.ConfirmResetPassword)]
        public async Task<IActionResult> ConfirmResetPassword([FromQuery] ConfirmResetPasswordQuery command)
        {
            return NewResult(await _mediator.Send(command));
        }


    }
}
