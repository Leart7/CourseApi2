using CourseApi.Models;

namespace CourseApi.Repositories
{
    public interface ICourseRepository
    {
        Task<(List<Course> Courses, int TotalCourses, int TotalPages)> GetCourses(string? name = null, string? category = null, string? status = null, int? pageNumber = 1);
        Task<Course?> GetCourse(int id);
        Task<List<Course>> GetPopularCourses();
        Task<List<Course>> GetCoursesByCategory(int categoryId);
        Task<Course> CreateCourse(Course course);
        Task<Course?> UpdateCourse(int id, Course course);
        Task<Course?> DeleteCourse(int id);
    }
}
