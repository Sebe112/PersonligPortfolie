using Microsoft.AspNetCore.Mvc;
using MyDAL.Models;
using MyDAL.Interfaces;
using MyApi.DTOs;

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
        public async Task<ActionResult<Skill>> AddSkill(SkillDTO skillDto)
        {
            var skill = new Skill
            {
                Name = skillDto.Name,
                Proficiency = skillDto.Proficiency
            };

            await _skillRepository.AddSkillAsync(skill);
            return CreatedAtAction(nameof(GetSkillById), new { id = skill.Id }, skill);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateSkill(int id, SkillUpdateDTO skillDto)
        {
            if (id != skillDto.Id)
            {
                return BadRequest("ID in URL and body must match.");
            }

            var existingSkill = await _skillRepository.GetSkillByIdAsync(id);
            if (existingSkill == null)
            {
                return NotFound();
            }
            existingSkill.Name = skillDto.Name;
            existingSkill.Proficiency = skillDto.Proficiency;

            await _skillRepository.UpdateSkillAsync(existingSkill);
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