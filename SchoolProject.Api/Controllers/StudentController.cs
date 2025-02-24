﻿using Microsoft.AspNetCore.Mvc;
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
        public async Task<IActionResult> GetStudentById([FromRoute]int id)
        {
            return NewResult(await _mediator.Send(new GetStudentByIdQuery(id)));
        }
        [HttpPost(Router.StudentRouting.Add)]
        public async Task<IActionResult> AddStudent([FromBody] AddStudentCommand command)
        {
            return NewResult(await _mediator.Send(command));
        }
    }
}
