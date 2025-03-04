using Microsoft.AspNetCore.Mvc;
using MyDAL.Models;
using MyDAL.Interfaces;
using MyApi.DTOs;

namespace MyApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProjectSkillController : ControllerBase
    {
        private readonly IProjectSkill _projectSkillRepository;

        public ProjectSkillController(IProjectSkill projectSkillRepository)
        {
            _projectSkillRepository = projectSkillRepository;
        }

        [HttpGet("project/{projectId}/skills")]
        public async Task<ActionResult<IEnumerable<Skill>>> GetSkillsByProjectId(int projectId)
        {
            var skills = await _projectSkillRepository.GetSkillsByProjectIdAsync(projectId);
            return Ok(skills);
        }

        [HttpGet("skill/{skillId}/projects")]
        public async Task<ActionResult<IEnumerable<Project>>> GetProjectsBySkillId(int skillId)
        {
            var projects = await _projectSkillRepository.GetProjectsBySkillIdAsync(skillId);
            return Ok(projects);
        }

        [HttpPost]
        public async Task<IActionResult> AddProjectSkill(ProjectSkillDTO projectSkillDto)
        {
            var projectSkill = new ProjectSkill
            {
                ProjectId = projectSkillDto.ProjectId,
                SkillId = projectSkillDto.SkillId
            };

            await _projectSkillRepository.AddProjectSkillAsync(projectSkill);
            return Ok(new { message = "Skill added to project successfully!" });
        }
    }
}