using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SchoolProject.Data.Entities;

namespace SchoolProject.Infrastructure.Configration
{
    public class Ins_SubjectConfiguration : IEntityTypeConfiguration<Ins_Subject>
    {
        public void Configure(EntityTypeBuilder<Ins_Subject> builder)
        {
            builder.HasKey(iss => new { iss.InsID, iss.SubID });
            builder.HasOne(iss => iss.Instructor)
                   .WithMany(i => i.Ins_Subjects)
                   .HasForeignKey(iss => iss.InsID);
            builder.HasOne(iss => iss.Subject)
                   .WithMany(s => s.Ins_Subjects)
                   .HasForeignKey(iss => iss.SubID);
        }
    }
}
