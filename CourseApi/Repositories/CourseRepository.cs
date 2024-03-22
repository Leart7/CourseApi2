using CourseApi.Constants;
using CourseApi.Context;
using CourseApi.Models;
using Microsoft.EntityFrameworkCore;

namespace CourseApi.Repositories
{
    public class CourseRepository : ICourseRepository
    {
        private readonly ApplicationDbContext _db;
        private readonly IWebHostEnvironment _webHostEnvironment;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public CourseRepository(ApplicationDbContext db, IWebHostEnvironment webHostEnvironment, IHttpContextAccessor httpContextAccessor)
        {
            _db = db;
            _webHostEnvironment = webHostEnvironment;
            _httpContextAccessor = httpContextAccessor;
        }
        public async Task<Course> CreateCourse(Course course)
        {
            Random random = new Random();
            double rating = Math.Round(new Random().NextDouble() * (5.0 - 1.0) + 1.0, 2);
            double runtime = Math.Round(new Random().NextDouble() * (80.0 - 3.0) + 3.0, 2);
            int lessons = random.Next(15, 300);
            int students = random.Next(5, 200000);
            int reviews = random.Next(2, 10000);

            course.Ratings = rating;
            course.Runtime = runtime;
            course.Lessons = lessons;
            course.Students = students;
            course.Reviews = reviews;

            if (course.Image != null)
            {
                string folderPath = Path.Combine(Directory.GetCurrentDirectory(), "CategoryIcons");
                string uniqueFileName = $"{Guid.NewGuid().ToString()}_{course.Image.FileName}";


                if (!Directory.Exists(folderPath))
                {
                    Directory.CreateDirectory(folderPath);
                }

                string filePath = Path.Combine(folderPath, uniqueFileName);
                using (var fileStream = new FileStream(filePath, FileMode.Create))
                {
                    course.Image.CopyTo(fileStream);
                }
                var baseUrl = $"{_httpContextAccessor.HttpContext.Request.Scheme}://{_httpContextAccessor.HttpContext.Request.Host}{_httpContextAccessor.HttpContext.Request.PathBase}";

                course.Cover = $"{baseUrl}/CategoryIcons/{uniqueFileName}";
            }

            await _db.Courses.AddAsync(course);
            await _db.SaveChangesAsync();

            await _db.Entry(course).Reference(c => c.Category).LoadAsync();

            return course;
        }


        public async Task<Course?> DeleteCourse(int id)
        {
            var course = await GetCourse(id);

            _db.Courses.Remove(course);
            await _db.SaveChangesAsync();
            return course;
        }

        public async Task<Course?> GetCourse(int id)
        {
            var course = await _db.Courses.Include(c => c.Category).FirstOrDefaultAsync(c => c.Id == id);
            if(course == null)
            {
                return null;
            }

            return course;
        }

        public async Task<(List<Course> Courses, int TotalCourses, int TotalPages)> GetCourses(string? name = null, string? category = null, string? status = null, int? pageNumber = 1)
        {
            var courses = _db.Courses.Include(c => c.Category).AsQueryable();

            if (!String.IsNullOrEmpty(name))
            {
                courses = courses.Where(c => c.Name.ToLower().Contains(name.ToLower()));
            }

            if (!String.IsNullOrEmpty(category))
            {
                courses = courses.Where(c => c.Category.Name.ToLower().Contains(category.ToLower()));
            }

            if (!String.IsNullOrEmpty(status))
            {
                var statuses = status.Split('&').Select(s => s.ToLower()).ToList();
                courses = courses.Where(c => statuses.Contains(c.Status.ToLower()));
            }

            var totalCourses = await courses.CountAsync();
            var pageSize = PageSize.SmallPage;

            var totalPages = (int)Math.Ceiling((double)totalCourses / (double)pageSize);
            if (totalPages == 0)
            {
                totalPages = 1;
            }

            if (pageNumber == null || pageNumber == 0 || pageNumber < 0)
            {
                pageNumber = 1;
            }

            if (pageNumber > totalPages)
            {
                pageNumber = totalPages;
            }

            var skipResults = (pageNumber - 1) * pageSize;

            var resultCourses = await courses.Skip(skipResults.Value).Take(pageSize).ToListAsync();

            var returnedObj = new
            {
                Courses = resultCourses,
                TotalCourses = totalCourses,
                TotalPages = totalPages,
            };

            return (resultCourses, totalCourses, totalPages);
        }

        public async Task<List<Course>> GetCoursesByCategory(int categoryId)
        {
            return await _db.Courses.Include(c => c.Category).Where(c => c.CategoryId == categoryId).ToListAsync();
        }

        public async Task<List<Course>> GetPopularCourses()
        {
            return await _db.Courses.OrderByDescending(c => c.Students).Take(3).ToListAsync();
        }

        public async Task<Course?> UpdateCourse(int id, Course course)
        {
            var existingcourse = await _db.Courses.Include(c => c.Category).FirstOrDefaultAsync(c => c.Id == id);
            if (existingcourse == null)
            {
                return null;
            }

            if (course.Image != null)
            {
                string folderPath = Path.Combine(Directory.GetCurrentDirectory(), "CategoryIcons");
                string uniqueFileName = $"{Guid.NewGuid().ToString()}_{course.Image.FileName}";


                if (!Directory.Exists(folderPath))
                {
                    Directory.CreateDirectory(folderPath);
                }

                string filePath = Path.Combine(folderPath, uniqueFileName);
                using (var fileStream = new FileStream(filePath, FileMode.Create))
                {
                    course.Image.CopyTo(fileStream);
                }
                var baseUrl = $"{_httpContextAccessor.HttpContext.Request.Scheme}://{_httpContextAccessor.HttpContext.Request.Host}{_httpContextAccessor.HttpContext.Request.PathBase}";

                existingcourse.Cover = $"{baseUrl}/CategoryIcons/{uniqueFileName}";
            }

            existingcourse.Name = course.Name;
            existingcourse.Description = course.Description;
            existingcourse.CategoryId = course.CategoryId;
            existingcourse.Teacher = course.Teacher;
            existingcourse.Language = course.Language;
            existingcourse.Status = course.Status;
            existingcourse.Video_Link = course.Video_Link;
            existingcourse.SkillLevel = course.SkillLevel;
            existingcourse.Certificate  = course.Certificate;
            existingcourse.Price = course.Price;
            existingcourse.Updated_at = DateTime.UtcNow;

            await _db.SaveChangesAsync();
            return course;
        }
    }
}
