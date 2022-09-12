using Microsoft.AspNetCore.Mvc;

namespace Server.Pages
{
	public class IndexModel : Infrastructure.BasePageModel
	{
		public IndexModel() : base()
		{
		}

		public IActionResult OnGet()
		{
			if(User == null ||
				User.Identity == null ||
				User.Identity.IsAuthenticated == false)         
			{
				return RedirectToPage("Account/Login");
			}

			return RedirectToPage("Admin/Index");
		}
	}
}
