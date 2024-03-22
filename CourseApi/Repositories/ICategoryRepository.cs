using CourseApi.Models;

namespace CourseApi.Repositories
{
    public interface ICategoryRepository
    {
        Task<List<(Category, int)>> GetCategories();
        Task<List<string>> GetCategoryNames();
        Task<Category> CreateCategory(Category category);
        Task<Category?> UpdateCategory(int id, Category category);
        Task<Category?> DeleteCategory(int id);
    }
}
