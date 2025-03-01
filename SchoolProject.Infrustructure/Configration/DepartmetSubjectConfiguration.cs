using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SchoolProject.Data.Entities;

namespace SchoolProject.Infrastructure.Configration
{
    public class DepartmetSubjectConfiguration : IEntityTypeConfiguration<DepartmetSubject>
    {
        public void Configure(EntityTypeBuilder<DepartmetSubject> builder)
        {
            builder.HasKey(ds => new { ds.DID, ds.SubID });
            builder.HasOne(ds => ds.Department)
                   .WithMany(d => d.DepartmentSubjects)
                   .HasForeignKey(ds => ds.DID);
            builder.HasOne(ds => ds.Subjects)
                   .WithMany(s => s.DepartmetsSubjects)
                   .HasForeignKey(ds => ds.SubID);
        }
    }
}
