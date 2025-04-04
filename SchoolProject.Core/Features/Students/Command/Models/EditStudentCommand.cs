﻿using MediatR;
using SchoolProject.Core.Bases;

namespace SchoolProject.Core.Features.Students.Command.Models
{
    public class EditStudentCommand : IRequest<Response<string>>
    {
        public int Id { get; set; }
        public string NameAr { get; set; }
        public string NameEn { get; set; }
        public string Address { get; set; }
        public string? Phone { get; set; }
        public int DepartmentID { get; set; }
    }
}
