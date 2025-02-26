using SchoolProject.Data.Entities;
using SchoolProject.Data.Helper.Enums;

namespace SchoolProject.Service.Abstract
{
    public interface IStudentService
    {
        public Task<List<Student>> GetStudentListAsync();
        public Task<Student> GetStudentByIdAsync(int id);
        public Task<string> AddAsync(Student student);
        public Task<bool> IsNameExistAsync(string name);
        public Task<bool> IsNameExistAsyncExcludeSelf(string name, int id);
        public Task<string> EditAsync(Student studentMapper);
        public Task<bool> DeleteAsync(Student student);
        public IQueryable<Student> GetPaginatedStudentListAsync();
        public IQueryable<Student> FilterStudentPaginatedQuerable(StudentOrderingEnum? orderingEnum, string search);
    }
}
