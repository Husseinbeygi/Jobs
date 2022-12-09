using Application.JobApp;
using Domain.CategoryAgg;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ViewModels.Pages.Admin.Job;
using ViewModels.Shared;
using static System.Net.Mime.MediaTypeNames;

namespace Server.Pages.Admin.Jobs;

[Microsoft.AspNetCore.Authorization.Authorize
    (Roles = Infrastructure.Constants.Role.Admin)]

public class CreateModel : Infrastructure.BasePageModel
{
    public CreateModel(ILogger<CreateModel> logger, IJobApplication jobApplication,
                       ICategoryRepository categoryRepository)
    {
        Logger = logger;
        JobApplication = jobApplication;
        CategoryRepository = categoryRepository;
        ViewModel = new();
        categories = new();
    }
    private ILogger<CreateModel> Logger { get; }

    private IJobApplication JobApplication { get; }

    private ICategoryRepository CategoryRepository { get; }

    public List<KeyValueViewModel> categories { get; set; }

    [BindProperty]
    public CommonViewModel ViewModel { get; set; }

    public async Task OnGetAsync()
    {
        var Categories = (await CategoryRepository.GetParents());
        foreach (var category in Categories)
        {
            categories.Add(new KeyValueViewModel()
            {
                Id = category.Id,
                Name = category.Name,
            });
        }
    }

    public async Task<IActionResult> OnPostAsync()
    {
        if (ModelState.IsValid == false)
        {
            return Page();
        }

        var res = await JobApplication.AddJob(ViewModel);

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
                (format: Resources.Messages.Successes.Created,
                arg0: Resources.DataDictionary.Job);

        AddToastSuccess(message: successMessage);

        return RedirectToPage("Index");
    }
}
