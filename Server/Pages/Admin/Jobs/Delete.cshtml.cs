using Application.JobApp;
using Application.UserApp;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;
using System.Threading.Tasks;
using System;
using ViewModels.Pages.Admin.Job;
using Domain.SeedWork;
using System.Linq;
using Application.CategoryApp;
using Framework.OperationResult;
using Domain.CategoryAgg;
using Application.OwnerApp;

namespace Server.Pages.Admin.Jobs;

[Microsoft.AspNetCore.Authorization.Authorize
    (Roles = Infrastructure.Constants.Role.Admin)]
public class DeleteModel : Infrastructure.BasePageModel
{
    public DeleteModel(ILogger<DeleteModel> logger, IJobApplication jobApplication,
                       ICategoryApplication categoryApplication, IOwnerApplication ownerApplication)
    {
        Logger = logger;
        JobApplication = jobApplication;
        CategoryApplication = categoryApplication;
        OwnerApplication = ownerApplication;
        ViewModel = new();
    }

    private ILogger<DeleteModel> Logger { get; }

    public IJobApplication JobApplication { get; }

    private ICategoryApplication CategoryApplication { get; }

    private IOwnerApplication OwnerApplication { get; }

    public OperationResultWithData<ViewModels.Pages.Admin.Categories.DetailsViewModel> category { get; set; }

    public OperationResultWithData<ViewModels.Pages.Admin.Owner.CommonViewModel> owner { get; set; }

    [BindProperty]
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

    public async Task<IActionResult> OnPostAsync(Guid? id)
    {
        if (id.HasValue == false)
        {
            AddToastError
                (message: Resources.Messages.Errors.IdIsNull);

            return RedirectToPage(pageName: "Index");
        }

        try
        {

            var res = (await JobApplication.DeleteJob(id.Value));

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
                (Resources.Messages.Successes.Deleted,
                Resources.DataDictionary.Job);

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
