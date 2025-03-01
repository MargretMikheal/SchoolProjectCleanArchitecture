using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SchoolProject.Data.Entities;

namespace SchoolProject.Infrastructure.Configration
{
    public class SubjectConfiguration : IEntityTypeConfiguration<Subject>
    {
        public void Configure(EntityTypeBuilder<Subject> builder)
        {
            builder.HasKey(s => s.SubID);
            builder.Property(s => s.SubID).ValueGeneratedOnAdd();
            builder.Property(s => s.SubjectNameAr).HasMaxLength(500);
            builder.Property(s => s.SubjectNameEn).HasMaxLength(500);
            builder.HasMany(s => s.StudentsSubjects)
                   .WithOne(ss => ss.Subject)
                   .HasForeignKey(ss => ss.SubID);
            builder.HasMany(s => s.DepartmetsSubjects)
                   .WithOne(ds => ds.Subjects)
                   .HasForeignKey(ds => ds.SubID);
            builder.HasMany(s => s.Ins_Subjects)
                   .WithOne(iss => iss.Subject)
                   .HasForeignKey(iss => iss.SubID);
        }
    }
}
