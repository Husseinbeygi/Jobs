using Application.AccountApp;
using Infrastructure;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using ViewModels.Pages.Account;

namespace Server.Pages.Security
{
    public class RegisterModel : BasePageModel
    {
        private readonly IAccountApplication _accountApplication;

        public RegisterModel(IAccountApplication accountApplication)
        {
            _accountApplication = accountApplication;
            ViewModel = new();
        }


        // **********
        [BindProperty]
        public RegisterViewModel ViewModel { get; set; }
        // **********

        public void OnGet()
        {
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (ModelState.IsValid == false)
            {
                return Page();
            }

            var res = await _accountApplication.RegisterUser(ViewModel);

            if (res.Succeeded == false || 0 < res.ErrorMessages.Count)
            {
                foreach (var error in res.ErrorMessages)
                {
                    AddPageError(error);
                }

                return Page();
            }

            return RedirectToPage("Login");
        }
    }
}
