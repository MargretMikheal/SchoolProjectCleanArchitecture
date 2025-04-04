﻿using SchoolProject.Data.Entities;

namespace SchoolProject.Service.Abstract
{
    public interface IDepartmentService
    {
        public Task<Department> GetDepartmentByIdAsync(int id);
        public Task<bool> IsExistAsync(int id);
    }
}
