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

    }
}
