using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CourseApi.Models
{
    public class Category : BaseModel
    {
        [Required]
        public string Name { get; set; }
        public string? Icon { get; set; }
        [NotMapped]
        public IFormFile Image { get; set; }
        public ICollection<Course> Courses { get; set; }
    }
}
