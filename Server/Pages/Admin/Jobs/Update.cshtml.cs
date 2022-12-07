using Application.JobApp;
using Application.UserApp;
using Domain.SeedWork;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ViewModels.Pages.Admin.Job;
using ViewModels.Shared;

namespace Server.Pages.Admin.Jobs;

[Microsoft.AspNetCore.Authorization.Authorize
    (Roles = Infrastructure.Constants.Role.Admin)]
public class UpdateModel : Infrastructure.BasePageModel
{
    public UpdateModel(ILogger<UpdateModel> logger,
        IJobApplication jobApplication)
    {
        Logger = logger;
        JobApplication = jobApplication;
        ViewModel = new();
    }

    private ILogger<UpdateModel> Logger { get; }
    public IJobApplication JobApplication { get; }

    [BindProperty]
    public UpdateViewModel ViewModel { get; set; }

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

    public async Task<IActionResult> OnPostAsync()
    {
        if (ModelState.IsValid == false)
        {
            return Page();
        }

        try
        {

            var res = await JobApplication.UpdateJob(ViewModel);

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
                (Resources.Messages.Successes.Updated,
                Resources.DataDictionary.User);

            AddToastSuccess(message: successMessage);

            return RedirectToPage(pageName: "Index");


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
