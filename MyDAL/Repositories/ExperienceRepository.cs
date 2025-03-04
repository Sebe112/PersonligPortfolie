using MyDAL.Interfaces;
using MyDAL.Models;
using MyApi.Data;
using Microsoft.EntityFrameworkCore;

namespace MyDAL.Repositories
{
    public class ExperienceRepository : IExperience
    {
        private readonly PortfolioDbContext _context;

        public ExperienceRepository(PortfolioDbContext context)
        {
            _context = context;
        }

        public async Task<List<Experience>> GetAllExperiencesAsync()
        {
            return await _context.Experiences.ToListAsync();
        }

        public async Task<Experience?> GetExperienceByIdAsync(int id)
        {
            return await _context.Experiences.FindAsync(id);
        }

        public async Task AddExperienceAsync(Experience experience)
        {
            _context.Experiences.Add(experience);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateExperienceAsync(Experience experience)
        {
            _context.Experiences.Update(experience);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteExperienceAsync(int id)
        {
            var experience = await _context.Experiences.FindAsync(id);
            if (experience != null)
            {
                _context.Experiences.Remove(experience);
                await _context.SaveChangesAsync();
            }
        }
    }
}