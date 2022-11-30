using System.ComponentModel.DataAnnotations;

namespace ViewModels.Pages.Admin.Categories
{
    public class CreateViewModel : CommonViewModel
    {
        // **********
        [Display
            (ResourceType = typeof(Resources.DataDictionary),
            Name = nameof(Resources.DataDictionary.Parent))]
        public Guid? ParentId { get; set; }
        // **********

        // **********
        public Guid EditorUserId { get; set; }
        // **********
    }
}