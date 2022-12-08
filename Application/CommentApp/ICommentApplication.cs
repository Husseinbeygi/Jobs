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
		Task<OperationResult> AddComment(CreateViewModel comment);
		Task<OperationResult> DeleteComment(Guid Id);
		Task<OperationResult> VerifyComment(Guid Id);
		Task<OperationResultWithData<DetailsViewModel>> GetComment(Guid Id);
		Task<OperationResultWithData<IList<IndexItemViewModel>>> GetAllComments();
		Task<OperationResultWithData<Comment>> GetCommentByUserId(Guid Id);

	}
}
