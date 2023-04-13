using Application.OwnerApp;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Collections.Generic;
using System.Threading.Tasks;
using ViewModels.Pages.Admin.Owner;

namespace Server.Pages.Admin.Owner;
    [Microsoft.AspNetCore.Authorization.Authorize
	(Roles = Infrastructure.Constants.Role.Admin)]

    public class IndexModel : Infrastructure.BasePageModel
{
        public IndexModel(IOwnerApplication ownerApplication)
        {
            OwnerApplication = ownerApplication;
        }

        private IOwnerApplication OwnerApplication { get; }

        public IList<CommonViewModel> ViewModel { get; set; }

        public async Task OnGetAsync()
        {
            ViewModel = (await OwnerApplication.GetAllOwners()).Data;
        }
    }

