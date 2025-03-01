using SchoolProject.Data.Commons;

namespace SchoolProject.Data.Entities
{
    public class Subject : GeneralLocalizableEntity
    {
        public Subject()
        {
            StudentsSubjects = new HashSet<StudentSubject>();
            DepartmetsSubjects = new HashSet<DepartmetSubject>();
            Ins_Subjects = new HashSet<Ins_Subject>();
        }

        public int SubID { get; set; }
        public string? SubjectNameAr { get; set; }
        public string? SubjectNameEn { get; set; }
        public int? Period { get; set; }

        public virtual ICollection<StudentSubject> StudentsSubjects { get; set; }
        public virtual ICollection<DepartmetSubject> DepartmetsSubjects { get; set; }
        public virtual ICollection<Ins_Subject> Ins_Subjects { get; set; }
    }
}