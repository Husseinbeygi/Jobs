using Application.JobApp;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Linq;
using System.Threading.Tasks;
using ViewModels.Pages.Admin.Job;

namespace Server.Pages.Admin.Jobs;

[Microsoft.AspNetCore.Authorization.Authorize
    (Roles = Infrastructure.Constants.Role.Admin)]

public class CreateModel : Infrastructure.BasePageModel
{
    public CreateModel(ILogger<CreateModel> logger, IJobApplication jobApplication)
    {
        Logger = logger;
        JobApplication = jobApplication;
        ViewModel = new();
        ViewModel.OpeningTime = Domain.SeedWork.Utility.Now;
        ViewModel.ClosingTime = Domain.SeedWork.Utility.Now;
    }
    private ILogger<CreateModel> Logger { get; }

    private IJobApplication JobApplication { get; }

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

        var res = await JobApplication.AddJob(ViewModel);

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
                arg0: Resources.DataDictionary.Job);

        AddToastSuccess(message: successMessage);

        return RedirectToPage("Index");
    }
}
