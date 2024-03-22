using CourseApi.Constants;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CourseApi.DTOs
{
    public class CreateCourseRequestDto
    {
        [Required]
        public string Name { get; set; }
        [Required]
        public string Description { get; set; }
        [Required]
        public string Teacher { get; set; } = "Ridvan Aliu";
        [Required]
        public string Video_Link { get; set; }
        [Required]
        [RegularExpression($"^({CourseStatuses.Free}|{CourseStatuses.Subscription}|{CourseStatuses.Locked}|{CourseStatuses.Paid})$")]
        public string Status { get; set; }
        [Required]
        public int CategoryId { get; set; }
        public double? Price { get; set; }
        public string? Language { get; set; }
        [RegularExpression($"^({SkillLevels.Beginner}|{SkillLevels.Intermediate}|{SkillLevels.Advanced}|{SkillLevels.Expert})$")]
        public string? SkillLevel { get; set; }
        public bool? Certificate { get; set; }
        [Required]
        public IFormFile Image { get; set; }
    }
}
