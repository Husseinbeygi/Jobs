using Domain.CategoryAgg;
using Domain.UserAgg;
using Microsoft.EntityFrameworkCore;

namespace Persistence
{
	public class DatabaseContext : DbContext
	{
		public DbSet<User> Users { get; set; }
		public DbSet<Category> Categories { get; set; }
		public DatabaseContext(DbContextOptions options) : base(options)
		{
			Database.EnsureCreated();	
		}

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

			modelBuilder.Entity<Category>()
				.HasOne(Category => Category.User)
				.WithMany(User => User.CategoriesEdited)
				.HasForeignKey(Category => Category.EditorUserId);
        }

    }
}
