using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SchoolProject.Data.Entities;

namespace SchoolProject.Infrastructure.Configration
{
    public class InstructorConfiguration : IEntityTypeConfiguration<Instructor>
    {
        public void Configure(EntityTypeBuilder<Instructor> builder)
        {
            builder.HasKey(i => i.InsID);
            builder.Property(i => i.InsID).ValueGeneratedOnAdd();
            builder.HasOne(i => i.Department)
                   .WithMany(d => d.Instructors)
                   .HasForeignKey(i => i.DID);
            builder.HasOne(i => i.Supervisor)
                   .WithMany(i => i.Instructors)
                   .HasForeignKey(i => i.SupervisorId);
            builder.HasMany(i => i.Ins_Subjects)
                   .WithOne(iss => iss.Instructor)
                   .HasForeignKey(iss => iss.InsID);
        }
    }
}
