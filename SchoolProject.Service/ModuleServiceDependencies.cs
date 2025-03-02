﻿using Microsoft.Extensions.DependencyInjection;
using SchoolProject.Service.Abstract;
using SchoolProject.Service.Implementation;

namespace SchoolProject.Service
{
    public static class ModuleServiceDependencies
    {
        public static IServiceCollection AddServiceDependencies(this IServiceCollection services)
        {
            services.AddTransient<IStudentService, StudentService>();
            services.AddTransient<IDepartmentService, DepartmentService>();
            return services;
        }
    }
}
