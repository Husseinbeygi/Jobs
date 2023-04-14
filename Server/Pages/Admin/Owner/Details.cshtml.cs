using Application.OwnerApp;
using Application.UserApp;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System;
using System.Threading.Tasks;
using ViewModels.Pages.Admin.Owner;

namespace Server.Pages.Admin.Owner;
[Microsoft.AspNetCore.Authorization.Authorize
    (Roles = Infrastructure.Constants.Role.Admin)]

public class DetailsModel : Infrastructure.BasePageModel
{
    public DetailsModel(IOwnerApplication ownerApplication)
    {
        OwnerApplication = ownerApplication;
        ViewModel = new();
    }

    private IOwnerApplication  OwnerApplication { get; }

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
}
