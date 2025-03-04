namespace MyApi.DTOs
{
    public class SkillDTO
    {
        public string Name { get; set; }
        public int Proficiency { get; set; }
    }

        public class SkillUpdateDTO
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int Proficiency { get; set; }
    }
}