using MyDAL.Interfaces;
using MyDAL.Models;
using MyApi.Data;
using Microsoft.EntityFrameworkCore;

namespace MyDAL.Repositories
{
    public class EducationRepository : IEducation
    {
        private readonly PortfolioDbContext _context;

        public EducationRepository(PortfolioDbContext context)
        {
            _context = context;
        }

        public async Task<List<Education>> GetAllEducationAsync()
        {
            return await _context.Educations.ToListAsync();
        }

        public async Task<Education?> GetEducationByIdAsync(int id)
        {
            return await _context.Educations.FindAsync(id);
        }

        public async Task AddEducationAsync(Education education)
        {
            _context.Educations.Add(education);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateEducationAsync(Education education)
        {
            _context.Educations.Update(education);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteEducationAsync(int id)
        {
            var education = await _context.Educations.FindAsync(id);
            if (education != null)
            {
                _context.Educations.Remove(education);
                await _context.SaveChangesAsync();
            }
        }
    }
}