using System.Linq;
using Microsoft.Extensions.Logging;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace Server.Pages.Admin.Users;

[Microsoft.AspNetCore.Authorization.Authorize
	(Roles = Infrastructure.Constants.Role.Admin)]
public class DetailsModel : Infrastructure.BasePageModel
{
	#region Constructor(s)
	public DetailsModel(ILogger<DetailsModel> logger)
	{
		Logger = logger;

		ViewModel = new();
	}
	#endregion /Constructor(s)

	#region Property(ies)
	// **********
	private Microsoft.Extensions.Logging.ILogger<DetailsModel> Logger { get; }
	// **********

	// **********
	public ViewModels.Pages.Admin.Users.DetailsOrDeleteViewModel ViewModel { get; private set; }
	// **********
	#endregion /Property(ies)

	#region OnGetAsync
	public async Task OnGetAsync()
	{
	}
	#endregion /OnGetAsync
}
