namespace MyDAL.Models
{
    public class Project
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string Technologies { get; set; }
        public string RepositoryUrl { get; set; }

        // Many-to-Many relationship med Skills
        public List<ProjectSkill> ProjectSkills { get; set; } = new List<ProjectSkill>();
    }
}