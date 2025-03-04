using MyDAL.Interfaces;
using MyDAL.Models;
using MyApi.Data;
using Microsoft.EntityFrameworkCore;

namespace MyDAL.Repositories
{
    public class ProjectSkillRepository : IProjectSkill
    {
        private readonly PortfolioDbContext _context;

        public ProjectSkillRepository(PortfolioDbContext context)
        {
            _context = context;
        }

        public async Task<List<Skill>> GetSkillsByProjectIdAsync(int projectId)
        {
            return await _context.ProjectSkills
                .Where(ps => ps.ProjectId == projectId)
                .Select(ps => ps.Skill)
                .ToListAsync();
        }

        public async Task<List<Project>> GetProjectsBySkillIdAsync(int skillId)
        {
            return await _context.ProjectSkills
                .Where(ps => ps.SkillId == skillId)
                .Select(ps => ps.Project)
                .ToListAsync();
        }

        public async Task AddProjectSkillAsync(ProjectSkill projectSkill)
        {
            _context.ProjectSkills.Add(projectSkill);
            await _context.SaveChangesAsync();
        }
    }
}