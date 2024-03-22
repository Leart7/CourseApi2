using System.ComponentModel.DataAnnotations;

namespace CourseApi.Models
{
    public class BaseModel
    {
        [Key]
        public int Id { get; set; }
        public DateTime Created_at { get; set; } = DateTime.UtcNow;
        public DateTime Updated_at { get; set; } = DateTime.UtcNow;
    }
}
