using Application.UserApp;
using Domain.SeedWork;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ViewModels.Pages.Admin.Users;
using ViewModels.Shared;
using Infrastructure;
using Resources;

namespace Server.Pages.Admin.Users;

[Authorize(Roles = Infrastructure.Constants.Role.Admin)]
public class UpdateModel : BasePageModel
{
	public UpdateModel(ILogger<UpdateModel> logger,
		IUserApplication userApplication)
	{
		Logger = logger;
		UserApplication = userApplication;
		ViewModel = new();

		RolesViewModel = new List<ValueNameViewModel>();

		var roles = typeof(Infrastructure.Constants.Role).GetFields();

		foreach (var role in roles)
		{
			RolesViewModel.Add(new ValueNameViewModel()
			{
				Name = role.Name,
				Value = role.Name,
			});
		}
	}


	private ILogger<UpdateModel> Logger { get; }
	public IUserApplication UserApplication { get; }
	public List<ValueNameViewModel> RolesViewModel { get; private set; }


	[BindProperty]
	public UpdateViewModel ViewModel { get; set; }

	public async Task<IActionResult> OnGetAsync(Guid? id)
	{

		try
		{
			if (id.HasValue == false)
			{
				AddToastError(Resources.Messages.Errors.IdIsNull);

				return RedirectToPage("Index");
			}

			ViewModel = (await UserApplication.GetUser(id.Value)).Data;

			if (ViewModel == null)
			{
				AddToastError(Resources.Messages.Errors.ThereIsNotAnyDataWithThisId);

				return RedirectToPage("Index");
			}

			return Page();
		}
		catch (Exception ex)
		{
			Logger.LogError(Domain.SeedWork.Constants.Logger.ErrorMessage, ex.Message);

			AddToastError(Resources.Messages.Errors.UnexpectedError);

			return RedirectToPage("Index");
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

			if (res.Succeeded == false || res.ErrorMessages.Count() > 0)
			{
				foreach (var item in res.ErrorMessages)
				{
					AddToastError(item);
				}

				return Page();
			}

			var successMessage = string.Format(Resources.Messages.Successes.Updated, DataDictionary.User);

			AddToastSuccess(successMessage);

			return RedirectToPage("Index");


		}
		catch (Exception ex)
		{
			Logger.LogError(Domain.SeedWork.Constants.Logger.ErrorMessage, ex.Message);

			AddToastError(Resources.Messages.Errors.UnexpectedError);

			return RedirectToPage("Index");
		}

	}
}
