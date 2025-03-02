using Microsoft.EntityFrameworkCore;
using SchoolProject.Data.Entities;
using SchoolProject.Infrastructure.Abstract;
using SchoolProject.Service.Abstract;

namespace SchoolProject.Service.Implementation
{
    public class DepartmentService : IDepartmentService
    {
        #region Fields
        public IDepartmentRepository _departmentRepository;
        #endregion
        #region Constructor
        public DepartmentService(IDepartmentRepository departmentRepository)
        {
            _departmentRepository = departmentRepository;
        }
        #endregion
        #region Methods
        public async Task<Department> GetDepartmentByIdAsync(int id)
        {
            return await _departmentRepository.GetTableNoTracking
                ().Where(x => x.DID == id)
                .Include(x => x.Students)
                .Include(x => x.DepartmentSubjects).ThenInclude(x => x.Subjects)
                .Include(x => x.Instructors).FirstOrDefaultAsync();

        }

        public async Task<bool> IsExistAsync(int id)
        {
            var result = await _departmentRepository.GetTableNoTracking().AnyAsync(x => x.DID == id);
            if (result)
            {
                return true;
            }
            return false;
        }
        #endregion
    }
}
