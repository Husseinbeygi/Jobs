using Domain.CommentAgg;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.CommentAgg
{
	public interface ICommentRepository : IRepository<Guid, Comment>
	{
		Task<IList<Comment>> GetAllAsync();
		Task<Comment> GetByUserId(Guid userId);
	}
}
