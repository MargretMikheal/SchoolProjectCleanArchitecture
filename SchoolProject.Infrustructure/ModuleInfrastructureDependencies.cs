using Microsoft.Extensions.DependencyInjection;
using SchoolProject.Data.Entities.Views;
using SchoolProject.Infrastructure.Abstract;
using SchoolProject.Infrastructure.Abstract.Views;
using SchoolProject.Infrastructure.InfrastructureBases;
using SchoolProject.Infrastructure.Repositories;
using SchoolProject.Service.Implementation.Views;

namespace SchoolProject.Infrastructure
{
    public static class ModuleInfrastructureDependencies
    {
        public static IServiceCollection AddInfrastructureDependencies(this IServiceCollection services)
        {
            services.AddTransient<IStudentRepository, StudentRepository>();
            services.AddTransient<IDepartmentRepository, DepartmentRepository>();
            services.AddTransient<IRefreshTokenRepository, RefreshTokenRepository>();
            services.AddTransient<ISubjectRepository, SubjectRepository>();
            services.AddTransient<IViewRepository<ViewDepartment>, ViewDepartmentRepository>();
            services.AddTransient(typeof(IGenericRepositoryAsync<>), typeof(GenericRepositoryAsync<>));
            return services;
        }
    }
}
