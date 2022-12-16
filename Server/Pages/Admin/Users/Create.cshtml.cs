using Application.UserApp;
using Infrastructure;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Resources;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ViewModels.Pages.Admin.Users;
using ViewModels.Shared;

namespace Server.Pages.Admin.Users
{

    [Authorize(Roles = Constants.Role.Admin)]
    public class CreateModel : BasePageModel
    {
        public CreateModel(IUserApplication userApplication)
        {
            UserApplication = userApplication;
            UserApplication = userApplication;
            ViewModel = new();

            RolesViewModel = new List<ValueNameViewModel>();
        }

        private IUserApplication UserApplication { get; }
        public IList<ValueNameViewModel> RolesViewModel { get; private set; }

        [BindProperty]
        public CreateViewModel ViewModel { get; set; }

        public async Task OnGetAsync()
        {
            var roles = typeof(Constants.Role).GetFields();

            foreach (var role in roles)
            {
                RolesViewModel.Add(new ValueNameViewModel()
                {
                    Name = role.Name,
                    Value = role.Name,
                });
            }
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (ModelState.IsValid == false)
            {
                return Page();
            }

            var res = await UserApplication.AddUser(ViewModel);

            if (res.Succeeded == false || res.ErrorMessages.Count() > 0)
            {
                foreach (var item in res.ErrorMessages)
                {
                    AddToastError(item);
                }

                return Page();
            }

            var successMessage = string.Format(Resources.Messages.Successes.Created, DataDictionary.User);

            AddToastSuccess(successMessage);

            return RedirectToPage("Index");
        }

    }
}