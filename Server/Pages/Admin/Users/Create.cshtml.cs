using Application.UserApp;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System.Threading.Tasks;
using ViewModels.Pages.Admin.Users;
using ViewModels.Shared;

namespace Server.Pages.Admin.Users;

[Microsoft.AspNetCore.Authorization.Authorize
	(Roles = Infrastructure.Constants.Role.Admin)]
public class CreateModel : Infrastructure.BasePageModel
{
	#region Constructor(s)
	public CreateModel(ILogger<CreateModel> logger,
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

	#region Property(ies)
	// **********
	private ILogger<CreateModel> Logger { get; }
	public IUserApplication UserApplication { get; }

	// **********

	// **********
	public IList
		<KeyValueViewModel> RolesViewModel
	{ get; private set; }
	// **********

	// **********
	[BindProperty]
	public CreateViewModel ViewModel { get; set; }
	// **********
	#endregion /Property(ies)

	#region OnGetAsync
	public async Task OnGetAsync()
	{
	}
	#endregion /OnGetAsync

	#region OnPostAsync
	public async Task<IActionResult> OnPostAsync()
	{
		if (ModelState.IsValid == false)
		{
			return Page();
		}

		var res = await UserApplication.AddUser(ViewModel);

		if (res.Succeeded)
		{
			var successMessage = string.Format
				(format: Resources.Messages.Successes.Created,
				arg0: Resources.DataDictionary.User);

			AddToastSuccess(message: successMessage);

			return RedirectToPage("Index");
		}

		foreach (var error in res.ErrorMessages)
		{
			AddToastError(error);
		}

		return Page();

	}
	#endregion OnPostAsync

}
