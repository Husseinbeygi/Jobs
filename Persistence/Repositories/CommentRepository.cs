using Domain.CommentAgg;
using Domain.UserAgg;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Persistence.Repositories
{
	public class CommentRepository : RepositoryBase<Guid, Comment>, ICommentRepository
	{
		private readonly DatabaseContext _context;
		public CommentRepository(DatabaseContext context) : base(context)
		{
			_context = context;
		}

		public async Task<List<Comment>> GetByUserId(Guid userId)
		{
			
			return await _context.Comments.Where(x => x.UserId == userId).ToListAsync();

		}
	}
}
