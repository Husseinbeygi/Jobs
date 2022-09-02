using System.Linq;
using Microsoft.Extensions.Logging;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace Server.Pages.Admin.Users;

[Microsoft.AspNetCore.Authorization.Authorize
	(Roles = Infrastructure.Constants.Role.Admin)]
public class IndexModel : Infrastructure.BasePageModel
{
	#region Constructor(s)
	public IndexModel(ILogger<IndexModel> logger) 
	{
		Logger = logger;

		ViewModel =
			new System.Collections.Generic.List
			<ViewModels.Pages.Admin.Users.IndexItemViewModel>();
	}
	#endregion /Constructor(s)

	#region Property(ies)
	// **********
	private Microsoft.Extensions.Logging.ILogger<IndexModel> Logger { get; }
	// **********

	// **********
	public System.Collections.Generic.IList
		<ViewModels.Pages.Admin.Users.IndexItemViewModel> ViewModel
	{ get; private set; }
	// **********
	#endregion /Property(ies)

	#region OnGetAsync
	public async Task OnGetAsync()
	{

	}
	#endregion /OnGetAsync
}
