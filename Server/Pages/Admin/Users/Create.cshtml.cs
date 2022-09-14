using Application.UserApp;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ViewModels.Pages.Admin.Users;
using ViewModels.Shared;

namespace Server.Pages.Admin.Users;

[Microsoft.AspNetCore.Authorization.Authorize
	(Roles = Infrastructure.Constants.Role.Admin)]
public class CreateModel : Infrastructure.BasePageModel
{
	public CreateModel(ILogger<CreateModel> logger,
		IUserApplication userApplication)
	{
		Logger = logger;
		UserApplication = userApplication;
		UserApplication = userApplication;
		ViewModel = new();

		RolesViewModel = new List<KeyValueViewModel>();
	}

	private ILogger<CreateModel> Logger { get; }
	private IUserApplication UserApplication { get; }

	public IList<KeyValueViewModel> RolesViewModel { get; private set; }

	[BindProperty]
	public CreateViewModel ViewModel { get; set; }

	public async Task OnGetAsync()
	{
	}

	public async Task<IActionResult> OnPostAsync()
	{
		if (ModelState.IsValid == false)
		{
			return Page();
		}

		var res = await UserApplication.AddUser(ViewModel);

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
				(format: Resources.Messages.Successes.Created,
				arg0: Resources.DataDictionary.User);

		AddToastSuccess(message: successMessage);

		return RedirectToPage("Index");

	}

}
