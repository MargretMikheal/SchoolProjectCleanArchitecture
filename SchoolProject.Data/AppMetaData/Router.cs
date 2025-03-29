namespace SchoolProject.Data.AppMetaData
{
    public static class Router
    {
        public const string root = "api";
        public const string version = "v1";
        public const string rule = root + "/" + version + "/";

        public static class StudentRouting
        {
            public const string Prefix = rule + "Student";
            public const string List = Prefix + "/AllStudents";
            public const string byId = Prefix + "/{id}";
            public const string Add = Prefix + "/AddStudent";
            public const string Edit = Prefix + "/EditStudent";
            public const string Delete = Prefix + "/{id}";
            public const string Paginated = Prefix + "/Paginated";
        }
        public static class DepartmentRouting
        {
            public const string Prefix = rule + "Department";
            public const string List = Prefix + "/AllDepartments";
            public const string byId = Prefix + "/{id}";
            public const string Add = Prefix + "/AddDepartment";
            public const string Edit = Prefix + "/EditDepartment";
            public const string Delete = Prefix + "/{id}";
            public const string Paginated = Prefix + "/Paginated";
        }
        public static class UserRouting
        {
            public const string Prefix = rule + "User";
            public const string List = Prefix + "/AllUsers";
            public const string byId = Prefix + "/{id}";
            public const string Add = Prefix + "/AddUser";
            public const string Edit = Prefix + "/EditUser";
            public const string Delete = Prefix + "/{id}";
            public const string Paginated = Prefix + "/Paginated";
            public const string ChangePassword = Prefix + "/ChangePassword";
        }

        public static class AuthenticationRouting
        {
            public const string Prefix = rule + "Authentication";
            public const string SignIn = Prefix + "/SignIn";
            public const string RefreshToken = Prefix + "/RefreshToken";
            public const string ValidateToken = Prefix + "/ValidateToken";
        }

        public static class AuthorizationRouting
        {
            public const string Prefix = rule + "Authorization";
            public const string Role = Prefix + "/Role";
            public const string Claim = Prefix + "/Claim";


            public const string AddRole = Role + "/AddRole";
            public const string EditRole = Role + "/EditRole";
            public const string DeleteRole = Role + "/{Id}";
            public const string GetAll = Role + "/GetAll";
            public const string GetById = Role + "/{Id}";
            public const string UserRoles = Role + "/UserRoles/{Id}";
            public const string UpdateUserRoles = Role + "/UpdateUserRoles";


            public const string UserClaims = Claim + "/UserClaims/{Id}";
            public const string UpdateUserClaims = Claim + "/UpdateUserClaims";
        }
    }
}
