using Microsoft.EntityFrameworkCore;
using SchoolProject.Data.Entities;
using SchoolProject.Infrastructure.Abstract;
using SchoolProject.Infrastructure.Data;
using SchoolProject.Infrastructure.InfrastructureBases;

namespace SchoolProject.Infrastructure.Repositories
{
    public class SubjectRepository : GenericRepositoryAsync<Subject>, ISubjectRepository
    {
        private DbSet<Subject> _subjects;
        public SubjectRepository(ApplicationDbContext dbContext) : base(dbContext)
        {
            _subjects = dbContext.Set<Subject>();
        }
    }
}
