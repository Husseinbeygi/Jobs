using Application.JobApp;
using Application.UserApp;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Threading.Tasks;
using System;
using ViewModels.Pages.Admin.Job;
using Domain.SeedWork;
using static Domain.SeedWork.Constants;

namespace Server.Pages.Admin.Jobs;

[Microsoft.AspNetCore.Authorization.Authorize
    (Roles = Infrastructure.Constants.Role.Admin)]

public class DetailsModel : Infrastructure.BasePageModel
{
    public DetailsModel(ILogger<DetailsModel> logger, IJobApplication jobApplication)
    {
        Logger = logger;
        JobApplication = jobApplication;
        ViewModel = new();
    }

    private ILogger<DetailsModel> Logger { get; }
    public IJobApplication JobApplication { get; }

    public DetailsViewModel ViewModel { get; private set; }

    public async Task<IActionResult> OnGetAsync(Guid? id)
    {
        try
        {
            if (id.HasValue == false)
            {
                AddToastError
                    (message: Resources.Messages.Errors.IdIsNull);

                return RedirectToPage(pageName: "Index");
            }

            ViewModel = (await JobApplication.GetJob(id.Value)).Data;

            if (ViewModel == null)
            {
                AddToastError
                    (message: Resources.Messages.Errors.ThereIsNotAnyDataWithThisId);

                return RedirectToPage(pageName: "Index");
            }

            return Page();
        }
        catch (System.Exception ex)
        {
            Logger.LogError
                (message: Constants.Logger.ErrorMessage, args: ex.Message);

            AddToastError
                (message: Resources.Messages.Errors.UnexpectedError);

            return RedirectToPage(pageName: "Index");
        }

    }
}
