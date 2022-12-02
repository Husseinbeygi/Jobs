using System.ComponentModel.DataAnnotations;

namespace ViewModels.Pages.Admin.Categories
{
    public class IndexViewModel : CommonViewModel
    {
        // **********
        public Guid Id { get; set; }
        // **********

        // **********
        public int ChildCount { get; set; }
        // **********
    }
}