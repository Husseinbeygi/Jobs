using Application.CategoryApp;
using Infrastructure;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using ViewModels.Pages.Admin.Categories;

namespace Server.Pages.Admin.Categories
{
    [Authorize(Roles = Constants.Role.Admin)]
    public class IndexModel : BasePageModel
    {
        private readonly ICategoryApplication _application;

        public IndexModel(ICategoryApplication application)
        {
            _application = application;
            ViewModel = new();
        }


        public List<IndexViewModel> ViewModel { get; private set; }

        public async Task<IActionResult> OnGetAsync(Guid? Id)
        {
            if (Id.HasValue)
            {
                var res = await _application.GetIndexCategories(Id.Value);

                if (!res.Succeeded || res.ErrorMessages.Count > 0)
                {
                    foreach (var error in res.ErrorMessages)
                    {
                        AddPageError(error);
                    }

                    return RedirectToPage("Index");
                }

                ViewModel = (await _application.GetIndexCategories(Id.Value)).Data;

                return Page();
            }

            ViewModel = (await _application.GetIndexCategories()).Data;

            return Page();
        }
    }
}
