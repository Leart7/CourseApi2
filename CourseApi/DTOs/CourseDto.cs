using CourseApi.Constants;
using System.ComponentModel.DataAnnotations;

namespace CourseApi.DTOs
{
    public class CourseDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string Teacher { get; set; } 
        public string Video_Link { get; set; }
        public string Status { get; set; }
        public int CategoryId { get; set; }
        public string Category { get; set; }
        public double Price { get; set; }
        public double Ratings { get; set; }
        public string Language { get; set; }
        public string SkillLevel { get; set; }
        public bool Certificate { get; set; }
        public double Runtime { get; set; }
        public int Lessons { get; set; }
        public int Students { get; set; }
        public int Reviews { get; set; }
        public string Cover { get; set; }
    }
}
