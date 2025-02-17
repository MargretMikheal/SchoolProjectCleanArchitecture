using Microsoft.EntityFrameworkCore;
using SchoolProject.Data.Entities;
using SchoolProject.Infrastructure.Abstract;
using SchoolProject.Infrastructure.Data;
using SchoolProject.Infrastructure.InfrastructureBases;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolProject.Infrastructure.Repositories
{
    public class StudentRepository :GenericRepositoryAsync<Student>, IStudentRepository
    {
        #region Fields
        private readonly DbSet<Student> _studentsRepository;
        #endregion

        #region Ctor
        public StudentRepository(ApplicationDbContext context): base(context)
        {
            _studentsRepository = context.Set<Student>();
        }
        #endregion

        #region Methods
        public async Task<List<Student>> GetStudentsListAsync()
        {
            return await _studentsRepository.Include(x=>x.Department).ToListAsync();
        }
        #endregion



    }
}
