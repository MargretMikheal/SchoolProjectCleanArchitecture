﻿using AutoMapper;

namespace SchoolProject.Core.Mapping.Students
{
    public partial class StudentProfile : Profile
    {
        public StudentProfile()
        {
            GetStudentListMapping();
            GetStudentByIdMapping();
            AddStudetnCommandMapping();
            EditStudetnCommandMapping();
            GetStudentPaginatedListMapping();
        }
    }
}
