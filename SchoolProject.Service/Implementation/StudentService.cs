using Microsoft.EntityFrameworkCore;
using SchoolProject.Data.Entities;
using SchoolProject.Data.Helper.Enums;
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

        public async Task<bool> IsNameExistAsyncExcludeSelf(string name, int id)
        {
            var stu = await _studentRepository.GetTableNoTracking().Where(x => x.Name.Equals(name) && !x.StudID.Equals(id)).FirstOrDefaultAsync();
            if (stu == null)
            {
                return false;
            }
            return true;
        }

        public async Task<string> EditAsync(Student studentMapper)
        {
            //Edit the student in the database
            await _studentRepository.UpdateAsync(studentMapper);
            return "Success";
        }

        public async Task<bool> DeleteAsync(Student student)
        {
            //Delete the student from the database
            var trans = _studentRepository.BeginTransaction();
            try
            {
                await _studentRepository.DeleteAsync(student);
                await trans.CommitAsync();
                return true;
            }
            catch (Exception ex)
            {
                trans.Rollback();
                return false;
            }

        }

        public IQueryable<Student> GetPaginatedStudentListAsync()
        {
            return _studentRepository.GetTableNoTracking().Include(x => x.Department).AsQueryable();
        }

        public IQueryable<Student> FilterStudentPaginatedQuerable(StudentOrderingEnum? orderingEnum, string search)
        {
            var querable = _studentRepository.GetTableNoTracking().Include(x => x.Department).AsQueryable();
            if (!string.IsNullOrEmpty(search))
            {
                querable = querable.Where(x => x.Name.Contains(search) || x.Address.Contains(search) || x.Department.DName.Contains(search));
            }
            switch (orderingEnum)
            {
                case StudentOrderingEnum.Name:
                    querable = querable.OrderBy(x => x.Name);
                    break;
                case StudentOrderingEnum.Address:
                    querable = querable.OrderBy(x => x.Address);
                    break;
                case StudentOrderingEnum.DepartmentName:
                    querable = querable.OrderBy(x => x.Department.DName);
                    break;
                default:
                    querable = querable.OrderBy(x => x.StudID);
                    break;
            }

            return querable;
        }
        #endregion




    }
}
