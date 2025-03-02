namespace SchoolProject.Core.Features.Students.Queries.Results
{
    public class GetStudentPaginatedListResponse
    {
        public int StudId { get; set; }
        public string Name { get; set; }
        public string Address { get; set; }
        public string? DepartmentName { get; set; }
        public GetStudentPaginatedListResponse()
        {
        }
        public GetStudentPaginatedListResponse(int studId, string name, string address, string? departmentName)
        {
            StudId = studId;
            Name = name;
            Address = address;
            DepartmentName = departmentName;
        }
    }
}
