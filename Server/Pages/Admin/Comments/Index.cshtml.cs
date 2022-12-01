using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using ViewModels.Pages.Admin.Comments;
using System.Collections.Generic;
using Application.CommentApp;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Application.UserApp;
using Microsoft.Build.Framework;
using Microsoft.Extensions.Logging;

namespace Server.Pages.Admin.Comments;

[Microsoft.AspNetCore.Authorization.Authorize
(Roles = Infrastructure.Constants.Role.Admin)]
public class IndexModel : Infrastructure.BasePageModel
{
	public IndexModel(ILogger<IndexModel> logger,
		ICommentApplication commentApplication)
	{
		Logger = logger;
		CommentApplication = commentApplication;
		ViewModel = new List<IndexItemViewModel>();
	}
	private ILogger<IndexModel> Logger { get; }
	public ICommentApplication CommentApplication { get; }
	public IList<IndexItemViewModel> ViewModel { get; private set; }


	public async Task OnGetAsync()
	{
		ViewModel = (await CommentApplication.GetAllComments()).Data;
	}
}

