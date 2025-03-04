using Microsoft.AspNetCore.Mvc;
using MyDAL.Models;
using MyDAL.Interfaces;

namespace MyApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EducationController : ControllerBase
    {
        private readonly IEducation _educationRepository;

        public EducationController(IEducation educationRepository)
        {
            _educationRepository = educationRepository;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Education>>> GetAllEducation()
        {
            var educationList = await _educationRepository.GetAllEducationAsync();
            return Ok(educationList);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Education>> GetEducationById(int id)
        {
            var education = await _educationRepository.GetEducationByIdAsync(id);
            if (education == null)
            {
                return NotFound();
            }
            return Ok(education);
        }

        [HttpPost]
        public async Task<ActionResult<Education>> AddEducation(Education education)
        {
            await _educationRepository.AddEducationAsync(education);
            return CreatedAtAction(nameof(GetEducationById), new { id = education.Id }, education);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateEducation(int id, Education education)
        {
            if (id != education.Id)
            {
                return BadRequest();
            }
            await _educationRepository.UpdateEducationAsync(education);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteEducation(int id)
        {
            var education = await _educationRepository.GetEducationByIdAsync(id);
            if (education == null)
            {
                return NotFound();
            }
            await _educationRepository.DeleteEducationAsync(id);
            return NoContent();
        }
    }
}