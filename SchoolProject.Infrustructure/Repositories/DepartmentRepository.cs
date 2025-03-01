using Microsoft.EntityFrameworkCore;
using SchoolProject.Data.Entities;
using SchoolProject.Infrastructure.Abstract;
using SchoolProject.Infrastructure.Data;
using SchoolProject.Infrastructure.InfrastructureBases;

namespace SchoolProject.Infrastructure.Repositories
{
    public class DepartmentRepository : GenericRepositoryAsync<Department>, IDepartmentRepository
    {
        #region Fields
        private DbSet<Department> _departments;
        #endregion
        #region Constructor
        public DepartmentRepository(ApplicationDbContext dbContext) : base(dbContext)
        {
            _departments = dbContext.Set<Department>();
        }
        #endregion
        #region Methods
        #endregion

    }
}
