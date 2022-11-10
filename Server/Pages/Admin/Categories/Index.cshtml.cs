using Application.CategoryApp;
using Infrastructure;
using Microsoft.AspNetCore.Authorization;
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


        public Dictionary<IndexViewModel, List<IndexViewModel>> ViewModel { get; private set; }

        public async Task OnGetAsync()
        {
            ViewModel = (await _application.GetGroupedCategories()).Data;
        }
    }
}
