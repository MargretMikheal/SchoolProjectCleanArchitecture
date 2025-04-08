using SchoolProject.Data.Entities;
using SchoolProject.Data.Entities.Views;

namespace SchoolProject.Service.Abstract
{
    public interface IDepartmentService
    {
        public Task<Department> GetDepartmentByIdAsync(int id);
        public Task<bool> IsExistAsync(int id);
        public Task<List<ViewDepartment>> GetViewDepartmentData();
    }
}
