using System.Linq;
using Microsoft.Extensions.Logging;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace Server.Pages.Admin.Users;

[Microsoft.AspNetCore.Authorization.Authorize
	(Roles = Infrastructure.Constants.Role.Admin)]
public class CreateModel : Infrastructure.BasePageModel
{
	#region Constructor(s)
	public CreateModel(ILogger<CreateModel> logger)
	{
		Logger = logger;

		ViewModel = new();

		RolesViewModel =
			new System.Collections.Generic.List
			<ViewModels.Shared.KeyValueViewModel>();
	}
	#endregion /Constructor(s)

	#region Property(ies)
	// **********
	private Microsoft.Extensions.Logging.ILogger<CreateModel> Logger { get; }
	// **********

	// **********
	public System.Collections.Generic.IList
		<ViewModels.Shared.KeyValueViewModel> RolesViewModel
	{ get; private set; }
	// **********

	// **********
	[Microsoft.AspNetCore.Mvc.BindProperty]
	public ViewModels.Pages.Admin.Users.CreateViewModel ViewModel { get; set; }
	// **********
	#endregion /Property(ies)

	#region OnGetAsync
	public async Task OnGetAsync()
	{
	}
	#endregion /OnGetAsync

	#region OnPostAsync
	public async Task OnPostAsync()
	{
	}
	#endregion OnPostAsync

}
