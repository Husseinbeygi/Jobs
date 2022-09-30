using Application.JobApp;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using ViewModels.Pages.Admin.Job;

namespace Server.Pages.Admin.Jobs
{
    public class IndexModel : Infrastructure.BasePageModel
    {
        public IndexModel(ILogger<IndexModel> logger, IJobApplication jobApplication)
        {
            Logger = logger;
            JobApplication = jobApplication;
            ViewModel = new List<DetailsViewModel>();
        }

        private ILogger<IndexModel> Logger { get; }
        public IJobApplication JobApplication { get; set; }
        public IList<DetailsViewModel> ViewModel { get; set; }

        public async void OnGetAsync()
        {
            ViewModel = (await JobApplication.GetAllJob()).Data;
        }
    }
}
