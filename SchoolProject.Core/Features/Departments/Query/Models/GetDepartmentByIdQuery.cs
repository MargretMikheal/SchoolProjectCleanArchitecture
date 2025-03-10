﻿using MediatR;
using SchoolProject.Core.Bases;
using SchoolProject.Core.Features.Departments.Query.Results;

namespace SchoolProject.Core.Features.Departments.Query.Models
{
    public class GetDepartmentByIdQuery : IRequest<Response<GetDepartmentByIdResponse>>
    {
        public int Id { get; set; }

        public GetDepartmentByIdQuery(int id)
        {
            Id = id;
        }
    }
}
