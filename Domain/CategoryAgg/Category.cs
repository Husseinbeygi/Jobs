using Domain.SeedWork;
using System.ComponentModel.DataAnnotations;

namespace Domain.CategoryAgg
{
    public class Category : 
        Entity,
        IEntityHasIsActive,
        IEntityHasIsDeletable
    {
        public Category()
        {
            Ordering = 1_000;
            IsActive = true;
            IsDeletable = false;
        }


        // **********
        [Required
            (AllowEmptyStrings = false,
            ErrorMessageResourceType = typeof(Resources.Messages.Validations),
            ErrorMessageResourceName = nameof(Resources.Messages.Validations.Required))]
        [Display
            (ResourceType = typeof(Resources.DataDictionary),
            Name = nameof(Resources.DataDictionary.Name))]
        public string Name { get; set; }
        // **********

        // **********
        [Display
            (ResourceType = typeof(Resources.DataDictionary),
            Name = nameof(Resources.DataDictionary.Parent))]
        public Guid? ParentId { get; set; }
        // **********

        // **********
        public bool IsActive { get; set; }
        // **********

        // **********
        public bool IsDeletable { get; set; }
        // **********
    }
}
