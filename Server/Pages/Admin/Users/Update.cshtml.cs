using System.Linq;
using Microsoft.Extensions.Logging;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace Server.Pages.Admin.Users;

[Microsoft.AspNetCore.Authorization.Authorize
	(Roles = Infrastructure.Constants.Role.Admin)]
public class UpdateModel : Infrastructure.BasePageModel
{
	#region Constructor(s)
	public UpdateModel(ILogger<UpdateModel> logger)
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
	private Microsoft.Extensions.Logging.ILogger<UpdateModel> Logger { get; }
	// **********

	// **********
	public System.Collections.Generic.List
		<ViewModels.Shared.KeyValueViewModel> RolesViewModel
	{ get; private set; }
	// **********

	// **********
	[Microsoft.AspNetCore.Mvc.BindProperty]
	public ViewModels.Pages.Admin.Users.UpdateViewModel ViewModel { get; set; }
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
    #endregion /OnPostAsync
}
