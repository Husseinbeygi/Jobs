using System.Linq;
using Microsoft.Extensions.Logging;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using ViewModels.Pages.Admin.Users;
using Application.UserApp;
using System;
using Domain.SeedWork;
using Persistence;
using System.Resources;

namespace Server.Pages.Admin.Users;

[Microsoft.AspNetCore.Authorization.Authorize
	(Roles = Infrastructure.Constants.Role.Admin)]
public class DeleteModel : Infrastructure.BasePageModel
{
	public DeleteModel(ILogger<DeleteModel> logger,
		IUserApplication userApplication)
	{
		Logger = logger;
		UserApplication = userApplication;
		ViewModel = new();
	}

	private ILogger<DeleteModel> Logger { get; }
	public IUserApplication UserApplication { get; }

	[BindProperty]
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

			ViewModel = (await UserApplication.GetUser(id.Value)).Data;

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

			var res = (await UserApplication.DeleteUser(id.Value));

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
				Resources.DataDictionary.User);

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
