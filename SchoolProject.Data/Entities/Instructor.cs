using SchoolProject.Data.Commons;

namespace SchoolProject.Data.Entities
{
    public class Instructor : GeneralLocalizableEntity
    {
        public Instructor()
        {
            Instructors = new HashSet<Instructor>();
            Ins_Subjects = new HashSet<Ins_Subject>();
        }

        public int InsID { get; set; }
        public string? NameAr { get; set; }
        public string? NameEn { get; set; }
        public string? Position { get; set; }
        public string? Address { get; set; }
        public decimal? Salary { get; set; }
        public int? SupervisorId { get; set; }
        public int? DID { get; set; }

        public Department? Department { get; set; }
        public Department? InsManager { get; set; }
        public Instructor? Supervisor { get; set; }
        public virtual ICollection<Instructor> Instructors { get; set; }
        public virtual ICollection<Ins_Subject> Ins_Subjects { get; set; }
    }
}