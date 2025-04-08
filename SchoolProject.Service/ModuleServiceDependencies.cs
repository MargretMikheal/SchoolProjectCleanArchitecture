using Microsoft.Extensions.DependencyInjection;
using SchoolProject.Service.Abstract;
using SchoolProject.Service.AuthService.Implementation;
using SchoolProject.Service.AuthService.Interface;
using SchoolProject.Service.Implementation;

namespace SchoolProject.Service
{
    public static class ModuleServiceDependencies
    {
        public static IServiceCollection AddServiceDependencies(this IServiceCollection services)
        {
            services.AddTransient<IStudentService, StudentService>();
            services.AddTransient<IDepartmentService, DepartmentService>();
            services.AddTransient<IAuthenticationService, AuthenticationService>();
            services.AddTransient<IAuthorizationService, AuthorizationService>();
            services.AddTransient<IEmailService, EmailService>();
            services.AddTransient<IInstructorService, InstructorService>();
            services.AddTransient<IApplicationUserService, ApplicationUserService>();
            services.AddTransient<ICurrentUserService, CurrentUserService>();
            return services;
        }
    }
}
