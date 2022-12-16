using Infrastructure;
using Microsoft.AspNetCore.Mvc;
using Resources.Messages;
using System.Linq;
using System.Security.Claims;

namespace Server.Pages
{
	public class IndexModel : BasePageModel
	{
		public IndexModel()
		{
		}

		public IActionResult OnGet()
		{
            if (User == null || User.Identity == null || User.Identity.IsAuthenticated == false)
            {
                return RedirectToPage("Account/Login");
            }

            var role = User.Claims.FirstOrDefault(x => x.Type == ClaimTypes.Role)?.Value;

            var fullName = User.Claims.FirstOrDefault(x => x.Type == "FullName")?.Value;

            if (role == Constants.Role.User)
            {
                AddToastSuccess(string.Format(Successes.Welcome, fullName));
                return RedirectToPage("User/Index");
            }
            else if (role == Constants.Role.Admin)
            {
                AddToastSuccess(string.Format(Successes.Welcome, fullName));
                return RedirectToPage("Admin/Index");
            }

            return RedirectToPage("Account/Login");
        }
	}
}
