using Domain.UserAgg;
using Microsoft.EntityFrameworkCore;

namespace Persistence.Repositories
{
	public class UserRepository : RepositoryBase<Guid, User>, IUserRepository
	{
		private readonly DatabaseContext _context;

		public UserRepository(DatabaseContext context) : base(context)
		{
			_context = context;
		}

		public async Task<IList<User>> GetAllAsync()
		{
			return await _context.Users.ToListAsync();

		}

		public async Task<User> GetByUserName(string userName)
		{
			return await _context.Users.FirstOrDefaultAsync(x => x.Username == userName);
		}
	}
}
