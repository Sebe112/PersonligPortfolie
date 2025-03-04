using MyDAL.Models;

namespace MyDAL.Interfaces
{
    public interface IProjectSkill
    {
        Task<List<Skill>> GetSkillsByProjectIdAsync(int projectId);
        Task<List<Project>> GetProjectsBySkillIdAsync(int skillId);
        Task AddProjectSkillAsync(ProjectSkill projectSkill);
    }
}