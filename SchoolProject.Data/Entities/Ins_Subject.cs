using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SchoolProject.Data.Entities
{
    public class Ins_Subject
    {
        [Key]
        public int InsID { get; set; }
        [Key]
        public int SubID { get; set; }

        [ForeignKey(nameof(InsID))]
        [InverseProperty(nameof(Instructor.Ins_Subjects))]
        public Instructor? Instructor { get; set; }

        [ForeignKey(nameof(SubID))]
        [InverseProperty(nameof(Subject.Ins_Subjects))]
        public Subject? Subject { get; set; }


    }
}
