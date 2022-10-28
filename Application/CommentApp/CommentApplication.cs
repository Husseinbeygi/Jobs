using Domain.CommentAgg;
using Framework.Password;
using Framework.OperationResult;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ViewModels.Pages.Admin.Comments;
using Resources.Messages;

namespace Application.CommentApp
{
	public class CommentApplication : ICommentApplication 
	{
		private readonly ICommentRepository _repository;
		public CommentApplication(ICommentRepository repository)
		{
			_repository = repository;
		}

		public async Task<OperationResult> DeleteComment(Guid Id)
		{
			var res = new OperationResult();
			var CommentForDelete = await _repository.GetAsync(Id);

			if (CommentForDelete == null)
			{
				res.AddErrorMessage
					(message: Errors.ThereIsNotAnyDataWithThisId);
			}

			if (res.ErrorMessages.Count > 0)
			{
				res.Succeeded = false;
				return res;
			}
			_repository.Remove(CommentForDelete);

			await _repository.SaveChangesAsync();
			res.Succeeded = true;
			return res;
		}

		public async Task<OperationResultWithData<Comment>> GetCommentByUserId(Guid Id)
		{
			var res = new OperationResultWithData<Comment>();

			var Comment = await _repository.GetByUserId(Id);

			res.Data = Comment;

			return res;
		}

		public async Task<OperationResultWithData<IndexItemViewModel>> GetComment(Guid Id)
		{
			var res = new OperationResultWithData<IndexItemViewModel>();

			var comment = await _repository.GetAsync(Id);

			var _comment = new IndexItemViewModel
			{
				Comment = comment?.Comments,
				Id = comment?.Id,
				InsertDateTime = comment?.InsertDateTime,
				IsVerified = comment.IsVerified,
				IsDeleted = comment.IsDeleted,
				IsEdited = comment.IsEdited,
				UpdateDateTime = comment.UpdateDateTime,
				Score = comment.Score,
			};

			res.Data = _comment;

			return res;
		}

		public async Task<OperationResultWithData<IList<IndexItemViewModel>>> GetAllComments()
		{
			var res = new OperationResultWithData<IList<IndexItemViewModel>>();

			var comments = await _repository.GetAllAsync();

			var _data = new List<IndexItemViewModel>();

			foreach (var comment in comments)
			{
				_data.Add(new IndexItemViewModel
				{
					Comment = comment?.Comments,
					Id = comment?.Id,
					InsertDateTime = comment?.InsertDateTime,
					IsVerified = comment.IsVerified,
					IsDeleted = comment.IsDeleted,
					IsEdited = comment.IsEdited,
					UpdateDateTime = comment.UpdateDateTime,
					Score = comment.Score,
				});
			}

			res.Data = _data;

			return res;
		}
	}
}
