using CourseApi.Context;
using CourseApi.Models;
using Microsoft.EntityFrameworkCore;

namespace CourseApi.Repositories
{
    public class CategoryRepository : ICategoryRepository
    {
        private readonly ApplicationDbContext _db;
        private readonly IWebHostEnvironment _webHostEnvironment;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public CategoryRepository(ApplicationDbContext db, IWebHostEnvironment webHostEnvironment, IHttpContextAccessor httpContextAccessor)
        {
            _db = db;
            _webHostEnvironment = webHostEnvironment;
            _httpContextAccessor = httpContextAccessor;
        }
        public async Task<Category> CreateCategory(Category category)
        {
            if (category.Image != null)
            {
                string folderPath = Path.Combine(Directory.GetCurrentDirectory(), "CategoryIcons");
                string uniqueFileName = $"{Guid.NewGuid().ToString()}_{category.Image.FileName}";


                if (!Directory.Exists(folderPath))
                {
                    Directory.CreateDirectory(folderPath);
                }

                string filePath = Path.Combine(folderPath, uniqueFileName);
                using (var fileStream = new FileStream(filePath, FileMode.Create))
                {
                    category.Image.CopyTo(fileStream);
                }
                var baseUrl = $"{_httpContextAccessor.HttpContext.Request.Scheme}://{_httpContextAccessor.HttpContext.Request.Host}{_httpContextAccessor.HttpContext.Request.PathBase}";

                category.Icon = $"{baseUrl}/CategoryIcons/{uniqueFileName}";
            }

            await _db.Categories.AddAsync(category);
            await _db.SaveChangesAsync();
            return category;
        }

        public async Task<Category?> DeleteCategory(int id)
        {
            var category = await _db.Categories.FirstOrDefaultAsync(c => c.Id == id);
            if (category == null)
            {
                return null;
            }

            _db.Categories.Remove(category);
            await _db.SaveChangesAsync();
            return category;
        }

        public async Task<List<(Category, int)>> GetCategories()
        {
            var categories = await _db.Categories
                .Select(c => new
                {
                    Category = c,
                    CoursesCount = c.Courses.Count()
                })
                .ToListAsync();

            return categories.Select(c => (c.Category, c.CoursesCount)).ToList();
        }

        public async Task<List<string>> GetCategoryNames()
        {
            return await _db.Categories.Select(c => c.Name.ToLower()).ToListAsync();
        }

        public async Task<Category?> UpdateCategory(int id, Category category)
        {
            var existingCategory = await _db.Categories.FirstOrDefaultAsync(c => c.Id == id);
            if (category == null)
            {
                return null;
            }

            if (category.Image != null)
            {
                string folderPath = Path.Combine(Directory.GetCurrentDirectory(), "CategoryIcons");
                string uniqueFileName = $"{Guid.NewGuid().ToString()}_{category.Image.FileName}";


                if (!Directory.Exists(folderPath))
                {
                    Directory.CreateDirectory(folderPath);
                }

                string filePath = Path.Combine(folderPath, uniqueFileName);
                using (var fileStream = new FileStream(filePath, FileMode.Create))
                {
                    category.Image.CopyTo(fileStream);
                }
                var baseUrl = $"{_httpContextAccessor.HttpContext.Request.Scheme}://{_httpContextAccessor.HttpContext.Request.Host}{_httpContextAccessor.HttpContext.Request.PathBase}";

                existingCategory.Icon = $"{baseUrl}/CategoryIcons/{uniqueFileName}";
            }

            existingCategory.Name = category.Name;
            existingCategory.Updated_at = DateTime.UtcNow;

            await _db.SaveChangesAsync();
            return category;
        }
    }
}
