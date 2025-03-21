﻿namespace SchoolProject.Core.Features.Departments.Query.Results
{
    public class GetDepartmentByIdResponse
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string ManagerName { get; set; }
        public List<StudentResponse>? StudentList { get; set; }
        public List<SubjectResponse>? SubjectList { get; set; }
        public List<InstructorResponse>? InstructorList { get; set; }
    }

    public class StudentResponse
    {
        public int Id { get; set; }
        public string StudentName { get; set; }
    }
    public class SubjectResponse
    {
        public int Id { get; set; }
        public string SubjectName { get; set; }
    }
    public class InstructorResponse
    {
        public int Id { get; set; }
        public string InstructorName { get; set; }
    }
}
