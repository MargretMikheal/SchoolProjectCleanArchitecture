using SchoolProject.Data.Entities;

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
    }
}
