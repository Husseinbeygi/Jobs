using Application.CommentApp;
using Microsoft.Extensions.Logging;
using ViewModels.Pages.Admin.Comments;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Threading.Tasks;
using Microsoft.Build.Framework;
using Application.UserApp;
using System.Linq;

namespace Server.Pages.Admin.Comments;

[Microsoft.AspNetCore.Authorization.Authorize
	(Roles = Infrastructure.Constants.Role.Admin)]
public class CreateModel : Infrastructure.BasePageModel
{
    public CreateModel(ILogger<CreateModel> logger,
        ICommentApplication commentApplication)
    {
        Logger = logger;
        CommentApplication = commentApplication;
        ViewModel = new();
    }

    private ILogger<CreateModel> Logger { get; }
    public ICommentApplication CommentApplication { get; }
    [BindProperty]
    public CreateViewModel ViewModel { get; set; }
    public async Task OnGetAsync()
    {
    }

    public async Task<IActionResult> OnPostAsync()
    {
		if (ModelState.IsValid == false)
		{
			return Page();
		}

		var res = await CommentApplication.AddComment(ViewModel);

		if (res.Succeeded == false ||
			res.ErrorMessages.Count() > 0)
		{
			foreach (var item in res.ErrorMessages)
			{
				AddToastError(item);
			}

			return Page();
		}

		var successMessage = string.Format
				(format: Resources.Messages.Successes.Created,
				arg0: Resources.DataDictionary.Comment);

		AddToastSuccess(message: successMessage);

		return RedirectToPage("Index");
	}
}
