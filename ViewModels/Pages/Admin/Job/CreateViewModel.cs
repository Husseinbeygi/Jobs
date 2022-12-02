using System.ComponentModel.DataAnnotations;

namespace ViewModels.Pages.Admin.Job
{
    public class CreateViewModel : CommonViewModel
    {
        // **********
        [Display(
        ResourceType = typeof (Resources.DataDictionary),
        Name = nameof (Resources.DataDictionary.OwnerId))]
        public Guid OwnerId { get; set; }
        // **********

        // **********
        [Display(
        ResourceType = typeof(Resources.DataDictionary),
        Name = nameof(Resources.DataDictionary.CategoryId))]
        public Guid CategoryId { get; set; }
        // **********
    }
}
