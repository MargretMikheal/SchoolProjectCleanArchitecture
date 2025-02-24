﻿using SchoolProject.Core.Features.Students.Queries.Results;
using SchoolProject.Data.Entities;


namespace SchoolProject.Core.Mapping.Students
{
    public partial class StudentProfile
    {
        public void GetStudentByIdMapping()
        {
            CreateMap<Student, GetStudentResponse>()
                .ForMember(dest => dest.DepartmentName, memberOptions => memberOptions.MapFrom(src => src.Department.DName));
        }
    }
}
