using Application.CategoryApp;
using Application.JobApp;
using Application.OwnerApp;
using Domain.CategoryAgg;
using Domain.SeedWork;
using Framework.OperationResult;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Threading.Tasks;
using ViewModels.Pages.Admin.Job;

namespace Server.Pages.Admin.Jobs;

[Microsoft.AspNetCore.Authorization.Authorize
    (Roles = Infrastructure.Constants.Role.Admin)]

public class DetailsModel : Infrastructure.BasePageModel
{
    public DetailsModel(ILogger<DetailsModel> logger, IJobApplication jobApplication,
                        ICategoryApplication categoryApplication,IOwnerApplication ownerApplication)
    {
        Logger = logger;
        JobApplication = jobApplication;
        CategoryApplication = categoryApplication;
        OwnerApplication = ownerApplication;
        ViewModel = new();
    }

    private ILogger<DetailsModel> Logger { get; }

    private IJobApplication JobApplication { get; }

    private ICategoryApplication CategoryApplication { get; }

    private IOwnerApplication OwnerApplication { get; }

    public OperationResultWithData<ViewModels.Pages.Admin.Categories.DetailsViewModel> category { get; set; }

    public OperationResultWithData<ViewModels.Pages.Admin.Owner.CommonViewModel> owner { get; set; }

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

            category = await CategoryApplication.GetCategory(ViewModel.CategoryId);

            owner = await OwnerApplication.GetOwner(ViewModel.OwnerId);

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
