namespace SchoolProject.Data.Entities
{
    public class Ins_Subject
    {
        public int InsID { get; set; }
        public int SubID { get; set; }

        public Instructor? Instructor { get; set; }
        public Subject? Subject { get; set; }
    }
}