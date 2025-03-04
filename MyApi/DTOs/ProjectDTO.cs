namespace MyApi.DTOs
{
    public class ProjectDTO
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public string Technologies { get; set; }
        public string RepositoryUrl { get; set; }
    }

        public class ProjectUpdateDTO
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string Technologies { get; set; }
        public string RepositoryUrl { get; set; }
    }
}
