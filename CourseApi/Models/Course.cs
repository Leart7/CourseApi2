using CourseApi.Constants;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace CourseApi.Models
{
    public class Course : BaseModel
    {
        [Required]
        public string Name { get; set; }
        [Required]
        public string Description { get; set; }
        [Required]
        public string Teacher { get; set; } = "Ridvan Aliu";
        [Required]
        [Url]
        public string Video_Link { get; set; }
        [Required]
        [RegularExpression($"^({CourseStatuses.Free}|{CourseStatuses.Subscription}|{CourseStatuses.Locked}|{CourseStatuses.Paid})$")]
        public string Status { get; set; }
        public double? Price { get; set; }
        public double? Ratings { get; set; }
        public string? Language { get; set; }
        [RegularExpression($"^({SkillLevels.Beginner}|{SkillLevels.Intermediate}|{SkillLevels.Advanced}|{SkillLevels.Expert})$")]
        public string? SkillLevel { get; set; }
        public bool? Certificate { get; set; }
        public double? Runtime { get; set; }
        public int? Lessons { get; set; }
        public int? Students { get; set; }
        public int? Reviews { get; set; }
        public string? Cover { get; set; }
        [NotMapped]
        public IFormFile Image { get; set; }
        public int? CategoryId { get; set; }

        public Category? Category { get; set; }
    }
}
