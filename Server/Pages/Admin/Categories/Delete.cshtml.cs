using Application.CategoryApp;
using Infrastructure;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Resources;
using Resources.Messages;
using System;
using System.Threading.Tasks;
using ViewModels.Pages.Admin.Categories;

namespace Server.Pages.Admin.Categories
{
    [Authorize(Roles = Constants.Role.Admin)]
    public class DeleteModel : BasePageModel
    {
        private readonly ICategoryApplication _application;

        public DeleteModel(ICategoryApplication application)
        {
            _application = application;
        }


        public DetailsViewModel ViewModel { get; private set; }

        public async Task<IActionResult> OnGetAsync(Guid? id)
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

            if (!ViewModel.IsDeletable)
            {
                var errorMessage = string.Format(Resources.Messages.Errors.UnableTo, ButtonCaptions.Delete, DataDictionary.Category);
                AddToastError(errorMessage);

                return RedirectToPage("Index");
            }

            return Page();
        }

        public async Task<IActionResult> OnPostAsync(Guid? id)
        {
            if (id == null)
            {
                AddToastError(Resources.Messages.Errors.IdIsNull);

                return RedirectToPage("Index");
            }

            var res = await _application.DeleteCategory(id.Value);

            if (!res.Succeeded || res.ErrorMessages.Count > 0)
            {
                foreach (var error in res.ErrorMessages)
                {
                    AddToastError(error);
                }

                return RedirectToPage("Index");
            }

            var successMessage = string.Format(Successes.Deleted, DataDictionary.Category);
            AddToastSuccess(successMessage);

            return RedirectToPage("Index");
        }

    }
}
