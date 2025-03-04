using Microsoft.AspNetCore.Mvc;
using MyDAL.Models;
using MyDAL.Interfaces;

namespace MyApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ExperienceController : ControllerBase
    {
        private readonly IExperience _experienceRepository;

        public ExperienceController(IExperience experienceRepository)
        {
            _experienceRepository = experienceRepository;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Experience>>> GetAllExperiences()
        {
            var experiences = await _experienceRepository.GetAllExperiencesAsync();
            return Ok(experiences);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Experience>> GetExperienceById(int id)
        {
            var experience = await _experienceRepository.GetExperienceByIdAsync(id);
            if (experience == null)
            {
                return NotFound();
            }
            return Ok(experience);
        }

        [HttpPost]
        public async Task<ActionResult<Experience>> AddExperience(Experience experience)
        {
            await _experienceRepository.AddExperienceAsync(experience);
            return CreatedAtAction(nameof(GetExperienceById), new { id = experience.Id }, experience);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateExperience(int id, Experience experience)
        {
            if (id != experience.Id)
            {
                return BadRequest();
            }
            await _experienceRepository.UpdateExperienceAsync(experience);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteExperience(int id)
        {
            var experience = await _experienceRepository.GetExperienceByIdAsync(id);
            if (experience == null)
            {
                return NotFound();
            }
            await _experienceRepository.DeleteExperienceAsync(id);
            return NoContent();
        }
    }
}