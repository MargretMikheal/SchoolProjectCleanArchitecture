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
        }

    }
}
