using Infrastructure;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Server.Pages.Admin
{
    [Authorize(Roles = Constants.Role.Admin)]
    public class IndexModel : PageModel
    {
        public void OnGet()
        {
        }
    }
}
