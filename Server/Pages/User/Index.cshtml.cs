using Infrastructure;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Server.Pages.User
{
    [Authorize]
    public class IndexModel : BasePageModel
    {
        public void OnGet()
        {
        }
    }
}
