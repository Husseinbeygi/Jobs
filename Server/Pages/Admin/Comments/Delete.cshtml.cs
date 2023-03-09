using Application.CommentApp;
using Domain.SeedWork;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;
using System;
using System.Linq;
using System.Threading.Tasks;
using ViewModels.Pages.Admin.Comments;

namespace Server.Pages.Admin.Comments;

[Microsoft.AspNetCore.Authorization.Authorize
(Roles = Infrastructure.Constants.Role.Admin)]
public class DeleteModel : Infrastructure.BasePageModel
{
	public DeleteModel(ILogger<DeleteModel> logger, ICommentApplication commentApplication)
	{
		Logger = logger;
		CommentApplication = commentApplication;
		ViewModel = new();
	}
	private ILogger<DeleteModel> Logger { get; }
	public ICommentApplication CommentApplication { get; }
	public DetailsViewModel ViewModel { get; private set; }
	public async Task<IActionResult> OnGetAsync(Guid? id)
    {
		try
		{
			if (id.HasValue == false)
			{
				AddToastError
					(message: Resources.Messages.Errors.IdIsNull);

				return RedirectToPage(pageName: "Index");
			}

			ViewModel = (await CommentApplication.GetComment(id.Value)).Data;

			if (ViewModel == null)
			{
				AddToastError
					(message: Resources.Messages.Errors.ThereIsNotAnyDataWithThisId);

				return RedirectToPage(pageName: "Index");
			}

			return Page();
		}
		catch (System.Exception ex)
		{
			Logger.LogError
				(message: Constants.Logger.ErrorMessage, args: ex.Message);

			AddToastError
				(message: Resources.Messages.Errors.UnexpectedError);

			return RedirectToPage(pageName: "Index");
		}
	}

	public async Task<IActionResult> OnPostAsync(Guid? id)
	{
		if (id.HasValue == false)
		{
			AddToastError
				(message: Resources.Messages.Errors.IdIsNull);

			return RedirectToPage(pageName: "Index");
		}

		try
		{

			var res = (await CommentApplication.DeleteComment(id.Value));

			if (res.Succeeded == false ||
				res.ErrorMessages.Count() > 0)
			{
				foreach (var item in res.ErrorMessages)
				{
					AddToastError(item);
				}

				return Page();
			}

			var successMessage = string.Format
				(Resources.Messages.Successes.Deleted,
				Resources.DataDictionary.Comment);

			AddToastSuccess(message: successMessage);

			return RedirectToPage(pageName: "Index");

		}
		catch (System.Exception ex)
		{
			Logger.LogError
				(message: Constants.Logger.ErrorMessage, args: ex.Message);

			AddToastError
				(message: Resources.Messages.Errors.UnexpectedError);

			return RedirectToPage(pageName: "Index");
		}

	}

}
