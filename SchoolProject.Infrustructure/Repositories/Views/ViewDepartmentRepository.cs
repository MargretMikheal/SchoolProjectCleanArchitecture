using Microsoft.EntityFrameworkCore;
using SchoolProject.Data.Entities.Views;
using SchoolProject.Infrastructure.Abstract.Views;
using SchoolProject.Infrastructure.Data;
using SchoolProject.Infrastructure.InfrastructureBases;

namespace SchoolProject.Service.Implementation.Views
{
    public class ViewDepartmentRepository : GenericRepositoryAsync<ViewDepartment>, IViewRepository<ViewDepartment>
    {
        #region Fields
        private DbSet<ViewDepartment> _viewDepartments;
        #endregion
        #region Constructor
        public ViewDepartmentRepository(ApplicationDbContext dbContext) : base(dbContext)
        {
            _viewDepartments = dbContext.Set<ViewDepartment>();
        }
        #endregion
    }
}
