namespace SchoolProject.Data.Helper.Dtos
{
    public class JwtAuthResult
    {
        public string AccessToken { get; set; }

        public RefreshToken refreshToken { get; set; }
    }
    public class RefreshToken
    {
        public string UserName { get; set; }
        public string RefreshTokenString { get; set; }
        public DateTime ExpireAt { get; set; }

    }
}
