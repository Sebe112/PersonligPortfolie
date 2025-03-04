using Microsoft.AspNetCore.Mvc;
using MyDAL.Models;
using MyDAL.Interfaces;

namespace MyApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SkillController : ControllerBase
    {
        private readonly ISkill _skillRepository;

        public SkillController(ISkill skillRepository)
        {
            _skillRepository = skillRepository;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Skill>>> GetAllSkills()
        {
            var skills = await _skillRepository.GetAllSkillsAsync();
            return Ok(skills);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Skill>> GetSkillById(int id)
        {
            var skill = await _skillRepository.GetSkillByIdAsync(id);
            if (skill == null)
            {
                return NotFound();
            }
            return Ok(skill);
        }

        [HttpPost]
        public async Task<ActionResult<Skill>> AddSkill(Skill skill)
        {
            await _skillRepository.AddSkillAsync(skill);
            return CreatedAtAction(nameof(GetSkillById), new { id = skill.Id }, skill);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateSkill(int id, Skill skill)
        {
            if (id != skill.Id)
            {
                return BadRequest();
            }
            await _skillRepository.UpdateSkillAsync(skill);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteSkill(int id)
        {
            var skill = await _skillRepository.GetSkillByIdAsync(id);
            if (skill == null)
            {
                return NotFound();
            }
            await _skillRepository.DeleteSkillAsync(id);
            return NoContent();
        }
    }
}