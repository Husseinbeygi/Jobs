using System.Linq;
using Microsoft.Extensions.Logging;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace Server.Pages.Admin.Users;

[Microsoft.AspNetCore.Authorization.Authorize
	(Roles = Infrastructure.Constants.Role.Admin)]
public class DeleteModel : Infrastructure.BasePageModel
{
	#region Constructor(s)
	public DeleteModel(ILogger<DeleteModel> logger)
	{
		Logger = logger;

		ViewModel = new();
	}
	#endregion /Constructor(s)

	#region Porperty(ies)
	// **********
	private Microsoft.Extensions.Logging.ILogger<DeleteModel> Logger { get; }
	// **********

	// **********
	[Microsoft.AspNetCore.Mvc.BindProperty]
	public ViewModels.Pages.Admin.Users.DetailsOrDeleteViewModel ViewModel { get; private set; }
	// **********
	#endregion /Porperty(ies)

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
