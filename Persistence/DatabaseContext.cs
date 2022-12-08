using Domain.JobAgg;
using Domain.CategoryAgg;
using Domain.UserAgg;
using Domain.CommentAgg;
using Microsoft.EntityFrameworkCore;

namespace Persistence
{
	public class DatabaseContext : DbContext
	{
		public DbSet<User> Users { get; set; }

		public DbSet<Job> Jobs { get; set; }
		public DbSet<Category> Categories { get; set; }
		public DbSet<Comment> Comments { get; set; }
		public DatabaseContext(DbContextOptions options) : base(options)
		{
			//Database.EnsureCreated();	
		}

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }

    }
}
