namespace SchoolProject.Data.Entities
{
    public class DepartmetSubject
    {
        public int DID { get; set; }
        public int SubID { get; set; }

        public virtual Department? Department { get; set; }
        public virtual Subject? Subjects { get; set; }
    }
}