using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SchoolProject.Data.Entities;

namespace SchoolProject.Infrastructure.Configration
{
    public class DepartmentConfiguration : IEntityTypeConfiguration<Department>
    {
        public void Configure(EntityTypeBuilder<Department> builder)
        {
            builder.HasKey(d => d.DID);
            builder.Property(d => d.DID).ValueGeneratedOnAdd();
            builder.Property(d => d.DNameAr).HasMaxLength(500);
            builder.HasOne(d => d.InsManager)
                   .WithOne(i => i.InsManager)
                   .HasForeignKey<Department>(d => d.InsManagerID);
            builder.HasMany(d => d.Students)
                   .WithOne(s => s.Department)
                   .HasForeignKey(s => s.DID);
            builder.HasMany(d => d.DepartmentSubjects)
                   .WithOne(ds => ds.Department)
                   .HasForeignKey(ds => ds.DID);
            builder.HasMany(d => d.Instructors)
                   .WithOne(i => i.Department)
                   .HasForeignKey(i => i.DID);
        }
    }
}
