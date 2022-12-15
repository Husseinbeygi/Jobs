using System.ComponentModel.DataAnnotations;

namespace ViewModels.Pages.Admin.Categories
{
    public class UpdateViewModel : CommonViewModel
    {
        // **********
        public Guid Id { get; set; }
        // **********

        // **********
        [Display
            (ResourceType = typeof(Resources.DataDictionary),
            Name = nameof(Resources.DataDictionary.Parent))]
        public Guid? ParentId { get; set; }
        // **********

        // **********
        [Display
            (ResourceType = typeof(Resources.DataDictionary),
            Name = nameof(Resources.DataDictionary.EditorUserId))]
        public Guid EditorUserId { get; set; }
        // **********
    }
}
