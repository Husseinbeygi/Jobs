using Domain.UserAgg;
using Microsoft.EntityFrameworkCore;

namespace Persistence
{
	public class DatabaseContext : DbContext
	{
		public DbSet<User> Users { get; set; }
		public DatabaseContext(DbContextOptions options) : base(options)
		{
			Database.EnsureCreated();	
		}

	}
}
