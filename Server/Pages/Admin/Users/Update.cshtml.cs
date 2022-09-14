using Application.UserApp;
using Domain.SeedWork;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ViewModels.Pages.Admin.Users;
using ViewModels.Shared;

namespace Server.Pages.Admin.Users;

[Microsoft.AspNetCore.Authorization.Authorize
	(Roles = Infrastructure.Constants.Role.Admin)]
public class UpdateModel : Infrastructure.BasePageModel
{
	#region Constructor(s)
	public UpdateModel(ILogger<UpdateModel> logger,
		IUserApplication userApplication)
	{
		Logger = logger;
		UserApplication = userApplication;
		ViewModel = new();

		RolesViewModel =
			new System.Collections.Generic.List
			<ViewModels.Shared.KeyValueViewModel>();
	}
	#endregion /Constructor(s)


	private ILogger<UpdateModel> Logger { get; }
	public IUserApplication UserApplication { get; }
	public List<KeyValueViewModel> RolesViewModel { get; private set; }


	[BindProperty]
	public UpdateViewModel ViewModel { get; set; }

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

	public async Task<IActionResult> OnPostAsync()
	{
		if (ModelState.IsValid == false)
		{
			return Page();
		}

		try
		{

			var res = await UserApplication.UpdateUser(ViewModel);

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
				(Resources.Messages.Successes.Updated,
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
