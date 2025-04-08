using Microsoft.EntityFrameworkCore;
using SchoolProject.Data.Entities;
using SchoolProject.Data.Entities.Views;
using SchoolProject.Infrastructure.Abstract;
using SchoolProject.Infrastructure.Abstract.Views;
using SchoolProject.Service.Abstract;

namespace SchoolProject.Service.Implementation
{
    public class DepartmentService : IDepartmentService
    {
        #region Fields
        public IDepartmentRepository _departmentRepository;
        private readonly IViewRepository<ViewDepartment> _viewDepartmentRepository;
        #endregion
        #region Constructor
        public DepartmentService(IDepartmentRepository departmentRepository, IViewRepository<ViewDepartment> viewDepartmentRepository)
        {
            _departmentRepository = departmentRepository;
            _viewDepartmentRepository = viewDepartmentRepository;
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

        public async Task<List<ViewDepartment>> GetViewDepartmentData()
        {
            var viewDepartment = _viewDepartmentRepository.GetTableNoTracking();
            return await viewDepartment.ToListAsync();
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
