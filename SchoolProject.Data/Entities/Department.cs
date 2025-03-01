using SchoolProject.Data.Commons;

namespace SchoolProject.Data.Entities
{
    public class Department : GeneralLocalizableEntity
    {
        public Department()
        {
            Students = new HashSet<Student>();
            DepartmentSubjects = new HashSet<DepartmetSubject>();
            Instructors = new HashSet<Instructor>();
        }

        public int DID { get; set; }
        public string? DNameAr { get; set; }
        public string? DNameEn { get; set; }
        public int? InsManagerID { get; set; }

        public virtual ICollection<Student> Students { get; set; }
        public virtual ICollection<DepartmetSubject> DepartmentSubjects { get; set; }
        public virtual ICollection<Instructor> Instructors { get; set; }
        public virtual Instructor? InsManager { get; set; }
    }
}