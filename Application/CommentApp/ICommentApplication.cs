using Domain.CommentAgg;
using Framework.OperationResult;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ViewModels.Pages.Admin.Comments;

namespace Application.CommentApp
{
	public interface ICommentApplication
	{
		Task<OperationResult> DeleteComment(Guid Id);
		Task<OperationResultWithData<VerifyViewModel>> GetComment(Guid Id);
		Task<OperationResultWithData<IList<IndexItemViewModel>>> GetAllComments();
		Task<OperationResultWithData<Comment>> GetCommentByUserId(Guid Id);

	}
}
