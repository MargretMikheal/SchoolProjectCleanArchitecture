namespace SchoolProject.Core.Features.User.Query.Results
{
    public class GetUserByIdResponse
    {
        public string Id { get; set; }
        public string UserName { get; set; }
        public string Email { get; set; }
        public string FullName { get; set; }
        public string PhoneNumber { get; set; }
    }
}
