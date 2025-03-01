using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SchoolProject.Data.Entities;

namespace SchoolProject.Infrastructure.Configration
{
    public class StudentConfiguration : IEntityTypeConfiguration<Student>
    {
        public void Configure(EntityTypeBuilder<Student> builder)
        {
            builder.HasKey(s => s.StudID);
            builder.Property(s => s.StudID).ValueGeneratedOnAdd();
            builder.Property(s => s.Address).HasMaxLength(500);
            builder.Property(s => s.Phone).HasMaxLength(500);
            builder.HasOne(s => s.Department)
                   .WithMany(d => d.Students)
                   .HasForeignKey(s => s.DID);
            builder.HasMany(s => s.StudentSubjects)
                   .WithOne(ss => ss.Student)
                   .HasForeignKey(ss => ss.StudID);
        }
    }
}
