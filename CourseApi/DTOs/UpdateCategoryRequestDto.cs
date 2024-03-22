using System.ComponentModel.DataAnnotations;

namespace CourseApi.DTOs
{
    public class UpdateCategoryRequestDto
    {
        public string? Name { get; set; }
        public IFormFile? Image { get; set; }
    }
}
