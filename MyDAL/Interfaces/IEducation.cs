using MyDAL.Models;

namespace MyDAL.Interfaces
{
    public interface IEducation
    {
        Task<List<Education>> GetAllEducationAsync();
        Task<Education?> GetEducationByIdAsync(int id);
        Task AddEducationAsync(Education education);
        Task UpdateEducationAsync(Education education);
        Task DeleteEducationAsync(int id);
    }
}