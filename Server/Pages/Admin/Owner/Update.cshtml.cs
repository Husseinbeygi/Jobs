using Application.OwnerApp;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;
using ViewModels.Pages.Admin.Owner;

namespace Server.Pages.Admin.Owner;

[Microsoft.AspNetCore.Authorization.Authorize
    (Roles = Infrastructure.Constants.Role.Admin)]

public class UpdateModel : Infrastructure.BasePageModel
{
    public UpdateModel(IOwnerApplication ownerApplication)
    {
        OwnerApplication = ownerApplication;
        ViewModel = new();
    }

    private IOwnerApplication OwnerApplication { get; }

    [BindProperty]
    public CommonViewModel ViewModel { get; set; }

    public async Task<IActionResult> OnGetAsync(Guid? id)
    {
        if (id.HasValue == false)
        {
            AddToastError
                (message: Resources.Messages.Errors.IdIsNull);

            return RedirectToPage(pageName: "Index");
        }

        ViewModel = (await OwnerApplication.GetOwner(id.Value)).Data;

        if (ViewModel == null)
        {
            AddToastError
                (message: Resources.Messages.Errors.ThereIsNotAnyDataWithThisId);

            return RedirectToPage(pageName: "Index");
        }

        return Page();
    }

    public async Task<IActionResult> OnPostAsync()
    {
        if (ModelState.IsValid == false)
        {
            return Page();
        }

        var res = await OwnerApplication.UpdateOwner(ViewModel);

        if (res.Succeeded == false ||
            res.ErrorMessages.Count > 0)
        {
            foreach (var item in res.ErrorMessages)
            {
                AddToastError(item);
            }

            return Page();
        }

        var successMessage = string.Format
            (Resources.Messages.Successes.Updated,
            Resources.DataDictionary.Owner);

        AddToastSuccess(message: successMessage);

        return RedirectToPage(pageName: "Index");
    }
}
