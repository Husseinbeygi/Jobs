using System.Linq;
using Microsoft.Extensions.Logging;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace Server.Pages.Security
{
	public class RegisterModel : Infrastructure.BasePageModel
	{
		public RegisterModel(ILogger<RegisterModel> logger) 
		{
			Logger = logger;
			ViewModel = new();
		}

		// **********
		[Microsoft.AspNetCore.Mvc.BindProperty]
		public ViewModels.Pages.Account.RegisterViewModel ViewModel { get; set; }
		// **********

		// **********
		private Microsoft.Extensions.Logging.ILogger<RegisterModel> Logger { get; }
		// **********

		public void OnGet()
		{
		}

		public async Task OnPostAsync()
		{

		}
	}
}
