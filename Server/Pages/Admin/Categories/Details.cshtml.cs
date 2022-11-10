using Application.CategoryApp;
using Infrastructure;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;
using ViewModels.Pages.Admin.Categories;

namespace Server.Pages.Admin.Categories
{
    [Authorize(Roles = Constants.Role.Admin)]
    public class DetailsModel : BasePageModel
    {
        private readonly ICategoryApplication _application;

        public DetailsModel(ICategoryApplication application)
        {
            _application = application;
            ViewModel = new();
        }


        public DetailsViewModel ViewModel { get; private set; }

        public async Task<IActionResult> OnGet(Guid? id)
        {
            if (!id.HasValue)
            {
                AddToastError(Resources.Messages.Errors.IdIsNull);

                return RedirectToPage("Index");
            }

            ViewModel = (await _application.GetCategory(id.Value)).Data;

            if (ViewModel == null)
            {
                AddToastError(Resources.Messages.Errors.ThereIsNotAnyDataWithThisId);

                return RedirectToPage("Index");
            }

            return Page();
        }
    }
}
