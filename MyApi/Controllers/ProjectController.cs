using Microsoft.AspNetCore.Mvc;
using MyDAL.Models;
using MyDAL.Interfaces;
using MyApi.DTOs;

namespace MyApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProjectController : ControllerBase
    {
        private readonly IProject _projectRepository;

        public ProjectController(IProject projectRepository)
        {
            _projectRepository = projectRepository;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Project>>> GetAllProjects()
        {
            var projects = await _projectRepository.GetAllProjectsAsync();
            return Ok(projects);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Project>> GetProjectById(int id)
        {
            var project = await _projectRepository.GetProjectByIdAsync(id);
            if (project == null)
            {
                return NotFound();
            }
            return Ok(project);
        }

        [HttpPost]
        public async Task<ActionResult<Project>> AddProject(ProjectDTO projectDto)
        {
            var project = new Project
            {
                Title = projectDto.Title,
                Description = projectDto.Description,
                Technologies = projectDto.Technologies,
                RepositoryUrl = projectDto.RepositoryUrl
            };

            await _projectRepository.AddProjectAsync(project);
            return CreatedAtAction(nameof(GetProjectById), new { id = project.Id }, project);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateProject(int id, ProjectUpdateDTO projectDto)
        {
            if (id != projectDto.Id)
            {
                return BadRequest("ID in URL and body must match.");
            }

            var existingProject = await _projectRepository.GetProjectByIdAsync(id);
            if (existingProject == null)
            {
                return NotFound();
            }

            existingProject.Title = projectDto.Title;
            existingProject.Description = projectDto.Description;
            existingProject.Technologies = projectDto.Technologies;
            existingProject.RepositoryUrl = projectDto.RepositoryUrl;

            await _projectRepository.UpdateProjectAsync(existingProject);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteProject(int id)
        {
            var project = await _projectRepository.GetProjectByIdAsync(id);
            if (project == null)
            {
                return NotFound();
            }
            await _projectRepository.DeleteProjectAsync(id);
            return NoContent();
        }
    }
}