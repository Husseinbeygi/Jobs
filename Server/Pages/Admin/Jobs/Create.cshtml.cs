using Application.JobApp;
using Application.OwnerApp;
using Domain.CategoryAgg;
using Domain.OwnerAgg;
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
                       ICategoryRepository categoryRepository, IOwnerRepository ownerRepository)
    {
        Logger = logger;
        JobApplication = jobApplication;
        CategoryRepository = categoryRepository;
        OwnerRepository = ownerRepository;
        ViewModel = new();
        categories = new();
        owners = new();
    }
    private ILogger<CreateModel> Logger { get; }

    private IJobApplication JobApplication { get; }

    private ICategoryRepository CategoryRepository { get; }

    private IOwnerRepository OwnerRepository { get; }

    public List<KeyValueViewModel> categories { get; set; }

    public List<KeyValueViewModel> owners { get; set; }

    [BindProperty]
    public int hour_open { get; set; }

    [BindProperty]
    public int minutes_open { get; set; }

    [BindProperty]
    public int hour_close { get; set; }

    [BindProperty]
    public int minutes_close { get; set; }

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

        var Owners = await OwnerRepository.GetAllAsync();
        foreach (var owner in Owners)
        {
            owners.Add(new KeyValueViewModel()
            {
                Id = owner.Id,
                Name = owner.LName,
            });
        }
    }

    public async Task<IActionResult> OnPostAsync()
    {
        if (ModelState.IsValid == false)
        {
            return Page();
        }

        ViewModel.OpeningTime = TimeSpan.Parse($"{ hour_open }:{minutes_open}:00");
        ViewModel.ClosingTime = TimeSpan.Parse($"{hour_close}:{minutes_close}:00");

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
