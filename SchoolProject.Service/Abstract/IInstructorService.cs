using SchoolProject.Data.Entities;

namespace SchoolProject.Service.Abstract
{
    public interface IInstructorService
    {
        Task<string> AddInstructorAsync(Instructor instructor);
        Task<bool> IsNameExistAsync(string name);
        Task<bool> SupervisorExistsAsync(int supervisorId);
    }
}
