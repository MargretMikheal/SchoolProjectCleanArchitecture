using Microsoft.EntityFrameworkCore;
using SchoolProject.Data.Entities;
using SchoolProject.Infrastructure.Data;
using SchoolProject.Service.Abstract;

namespace SchoolProject.Service.Implementation
{
    public class InstructorService : IInstructorService
    {
        private readonly ApplicationDbContext _context;

        public InstructorService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<string> AddInstructorAsync(Instructor instructor)
        {
            _context.Instructors.Add(instructor);
            await _context.SaveChangesAsync();
            return "Success";
        }

        public async Task<bool> IsNameExistAsync(string name)
        {
            var instructor = await _context.Instructors
                .Where(x => x.NameAr == name || x.NameEn == name)
                .FirstOrDefaultAsync();
            return instructor != null;
        }

        public async Task<bool> SupervisorExistsAsync(int supervisorId)
        {
            var result = await _context.Instructors.AsNoTracking().AnyAsync(x => x.SupervisorId == supervisorId);
            if (result)
            {
                return true;
            }
            return false;
        }
    }
}

