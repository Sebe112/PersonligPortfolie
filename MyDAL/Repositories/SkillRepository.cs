using MyDAL.Interfaces;
using MyDAL.Models;
using MyApi.Data;
using Microsoft.EntityFrameworkCore;

namespace MyDAL.Repositories
{
    public class SkillRepository : ISkill
    {
        private readonly PortfolioDbContext _context;

        public SkillRepository(PortfolioDbContext context)
        {
            _context = context;
        }

        public async Task<List<Skill>> GetAllSkillsAsync()
        {
            return await _context.Skills.ToListAsync();
        }

        public async Task<Skill?> GetSkillByIdAsync(int id)
        {
            return await _context.Skills.FindAsync(id);
        }

        public async Task<List<Skill>> GetSkillsByProficiencyAsync(int minLevel)
        {
            return await _context.Skills
                .Where(s => s.Proficiency >= minLevel)
                .ToListAsync();
        }

        public async Task AddSkillAsync(Skill skill)
        {
            _context.Skills.Add(skill);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateSkillAsync(Skill skill)
        {
            _context.Skills.Update(skill);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteSkillAsync(int id)
        {
            var skill = await _context.Skills.FindAsync(id);
            if (skill != null)
            {
                _context.Skills.Remove(skill);
                await _context.SaveChangesAsync();
            }
        }
    }
}