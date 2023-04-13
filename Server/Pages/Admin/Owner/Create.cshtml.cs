using Application.OwnerApp;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using ViewModels.Pages.Admin.Owner;

namespace Server.Pages.Admin.Owner;
[Microsoft.AspNetCore.Authorization.Authorize
    (Roles = Infrastructure.Constants.Role.Admin)]

public class CreateModel : Infrastructure.BasePageModel
{

    public CreateModel(IOwnerApplication ownerApplication)
    {
        OwnerApplication = ownerApplication;
        ViewModel = new();
    }

    private IOwnerApplication OwnerApplication { get; }

    [BindProperty]
    public CommonViewModel ViewModel { get; set; }

    public async Task<IActionResult> OnGetAsync()
    {
        if (ModelState.IsValid == false)
        {
            return Page();
        }

        var res = await OwnerApplication.AddOwner(ViewModel);

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
                (format: Resources.Messages.Successes.Created,
                arg0: Resources.DataDictionary.Owner);

        AddToastSuccess(message: successMessage);

        return RedirectToPage("Index");
    }
}

