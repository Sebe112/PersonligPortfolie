namespace MyDAL.Models
{
    public class Skill
    {
        public int Id { get; set; }
        public string Name { get; set; } // Fx "C#", "JavaScript", "SQL"
        public int Proficiency { get; set; } // Fx 1-10 skala
    }
}