using Application.CategoryApp;
using Infrastructure;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Resources;
using Resources.Messages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ViewModels.Pages.Admin.Categories;
using ViewModels.Shared;

namespace Server.Pages.Admin.Categories
{
    [Authorize(Roles = Constants.Role.Admin)]
    public class UpdateModel : BasePageModel
    {
        private readonly ICategoryApplication _application;

        public UpdateModel(ICategoryApplication application)
        {
            _application = application;
            ParentsViewModel = new();
        }


        [BindProperty]
        public UpdateViewModel ViewModel { get; set; }
        public List<KeyValueViewModel> ParentsViewModel { get; set; }

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

            if (ViewModel.ParentId.HasValue)
            {
                var parents = (await _application.GetParentCategories()).Data;

                foreach (var parent in parents)
                {
                    if (parent.Id != ViewModel.ParentId)
                    {
                        ParentsViewModel.Add(new KeyValueViewModel()
                        {
                            Id = parent.Id,
                            Name = parent.Name,
                        });
                    }
                }

                ParentsViewModel.Insert(0, new KeyValueViewModel()
                {
                    Id = ViewModel.ParentId,
                    Name = (await _application.GetCategory(ViewModel.ParentId.Value)).Data?.Name,
                });

                ParentsViewModel.Insert(1, new KeyValueViewModel()
                {
                    Id = null,
                    Name = DataDictionary.WithoutParent,
                });
            }

            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            var editorUserId = User.Claims.FirstOrDefault(x => x.Type == "Id").Value;
            Console.WriteLine(editorUserId);
            ViewModel.EditorUserId = Guid.Parse(editorUserId);
            var res = await _application.UpdateCategory(ViewModel);

            if (!res.Succeeded || res.ErrorMessages.Count > 0)
            {
                foreach (var error in res.ErrorMessages)
                {
                    AddToastError(error);
                }

                return RedirectToPage("Index");
            }

            var successMessage = string.Format(Successes.Updated, DataDictionary.Category);
            AddToastSuccess(successMessage);

            return RedirectToPage("Index");
        }
    }
}
