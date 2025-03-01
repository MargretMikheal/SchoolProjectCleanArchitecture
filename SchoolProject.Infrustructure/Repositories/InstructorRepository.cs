using Microsoft.EntityFrameworkCore;
using SchoolProject.Data.Entities;
using SchoolProject.Infrastructure.Abstract;
using SchoolProject.Infrastructure.Data;
using SchoolProject.Infrastructure.InfrastructureBases;

namespace SchoolProject.Infrastructure.Repositories
{
    public class InstructorRepository : GenericRepositoryAsync<Instructor>, IInstructorsRepository
    {
        private DbSet<Instructor> _instructors;
        public InstructorRepository(ApplicationDbContext context) : base(context)
        {
            _instructors = context.Set<Instructor>();
        }

    }

}
