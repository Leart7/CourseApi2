using CourseApi.Constants;
using System.ComponentModel.DataAnnotations;

namespace CourseApi.DTOs
{
    public class UpdateCourseRequestDto
    {
        public string? Name { get; set; }
        public string? Description { get; set; }
        public string? Teacher { get; set; } = "Ridvan Aliu";
        public string? Video_Link { get; set; }
        
        [RegularExpression($"^({CourseStatuses.Free}|{CourseStatuses.Subscription}|{CourseStatuses.Locked}|{CourseStatuses.Paid})$")]
        public string Status { get; set; }
        public int? CategoryId { get; set; }
        public double? Price { get; set; }
        public string? Language { get; set; }
        [RegularExpression($"^({SkillLevels.Beginner}|{SkillLevels.Intermediate}|{SkillLevels.Advanced}|{SkillLevels.Expert})$")]
        public string? SkillLevel { get; set; }
        public bool? Certificate { get; set; }
        public IFormFile? Image { get; set; }
    }
}
