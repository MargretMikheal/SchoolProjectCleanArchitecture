using Microsoft.EntityFrameworkCore;
using SchoolProject.Data.Entities;

namespace SchoolProject.Infrastructure.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext() { }
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }
        public DbSet<Student> Students { get; set; }
        public DbSet<Department> Departments { get; set; }
        public DbSet<DepartmetSubject> DepartmetSubjects { get; set; }
        public DbSet<StudentSubject> StudentSubjects { get; set; }
        public DbSet<Subject> Subjects { get; set; }
        public DbSet<Instructor> Instructors { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<DepartmetSubject>()
                .HasKey(ds => new { ds.DID, ds.SubID });
            modelBuilder.Entity<StudentSubject>()
                .HasKey(ss => new { ss.StudID, ss.SubID });
            modelBuilder.Entity<Ins_Subject>()
                .HasKey(ins => new { ins.InsID, ins.SubID });

            modelBuilder.Entity<Instructor>()
                .HasOne(i => i.Supervisor)
                .WithMany(d => d.Instructors)
                .HasForeignKey(i => i.SupervisorId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Department>()
                .HasOne(d => d.InsManager)
                .WithOne(i => i.InsManager)
                .HasForeignKey<Department>(i => i.InsManagerID)
                .OnDelete(DeleteBehavior.Restrict);

            base.OnModelCreating(modelBuilder);
        }


    }
}
