using Microsoft.EntityFrameworkCore;
using SchoolProject.Data.Entities;
using SchoolProject.Infrastructure.Abstract;
using SchoolProject.Service.Abstract;

namespace SchoolProject.Service.Implementation
{
    public class StudentService : IStudentService
    {
        #region Fields
        private readonly IStudentRepository _studentRepository;
        #endregion

        #region Ctor
        public StudentService(IStudentRepository studentRepository)
        {
            _studentRepository = studentRepository;
        }


        #endregion
        #region Methods

        public async Task<Student> GetStudentByIdAsync(int id)
        {
            return await _studentRepository.GetTableNoTracking()
                .Include(x => x.Department)
                .FirstOrDefaultAsync(x => x.StudID == id);
        }

        public async Task<List<Student>> GetStudentListAsync()
        {
            return await _studentRepository.GetStudentsListAsync();
        }

        public async Task<string> AddAsync(Student student)
        {
            //Add the student to the database
            await _studentRepository.AddAsync(student);
            return "Success";
        }

        public async Task<bool> IsNameExistAsync(string name)
        {
            var stu = await _studentRepository.GetTableNoTracking().Where(x => x.Name == name).FirstOrDefaultAsync();
            if (stu == null)
            {
                return false;
            }
            return true;
        }
        #endregion




    }
}
