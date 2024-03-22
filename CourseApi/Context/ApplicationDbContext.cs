using CourseApi.Models;
using Microsoft.EntityFrameworkCore;

namespace CourseApi.Context
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Category>()
             .HasMany(category => category.Courses)  
             .WithOne(course => course.Category)    
             .HasForeignKey(course => course.CategoryId)  
             .OnDelete(DeleteBehavior.SetNull);
        }


        public DbSet<Course> Courses { get; set; }
        public DbSet<Category> Categories { get; set; }

    }
}
