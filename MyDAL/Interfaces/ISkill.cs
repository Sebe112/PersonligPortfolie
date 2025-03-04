using MyDAL.Models;

namespace MyDAL.Interfaces
{
    public interface ISkill
    {
        Task<List<Skill>> GetAllSkillsAsync();
        Task<Skill?> GetSkillByIdAsync(int id);
        Task<List<Skill>> GetSkillsByProficiencyAsync(int minLevel);
        Task AddSkillAsync(Skill skill);
        Task UpdateSkillAsync(Skill skill);
        Task DeleteSkillAsync(int id);
    }
}