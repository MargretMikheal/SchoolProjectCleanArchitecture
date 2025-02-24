using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
        }

    }
}
