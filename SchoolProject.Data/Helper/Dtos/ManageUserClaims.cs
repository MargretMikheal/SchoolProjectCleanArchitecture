namespace SchoolProject.Data.Helper.Dtos
{
    public class ManageUserClaims
    {
        public string UserId { get; set; }
        public List<Claims> Claims { get; set; }
    }
    public class Claims
    {
        public string Type { get; set; }
        public bool HasClaim { get; set; }
    }
}
