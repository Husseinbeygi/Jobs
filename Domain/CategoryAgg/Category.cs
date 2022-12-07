using Domain.SeedWork;
using Domain.UserAgg;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.CategoryAgg
{
    public class Category : 
        Entity,
        IEntityHasIsActive,
        IEntityHasIsDeletable,
        IEntityHasEditorUserId
    {
        public Category()
        {
            Ordering = 10_000;
            IsActive = true;
            IsDeletable = false;
            UpdateDateTime = InsertDateTime;
        }


        // **********
        [Required
            (AllowEmptyStrings = false,
            ErrorMessageResourceType = typeof(Resources.Messages.Validations),
            ErrorMessageResourceName = nameof(Resources.Messages.Validations.Required))]
        [Display
            (ResourceType = typeof(Resources.DataDictionary),
            Name = nameof(Resources.DataDictionary.Name))]
        [MaxLength(Constants.MaxLength.Name2)]
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

        // **********
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public DateTime UpdateDateTime { get; private set; }
        // **********


        // **********
        public Guid EditorUserId { get; set; }
        public User EditorUser { get; set; }
        // **********

        public void SetUpdateDateTime()
        {
            UpdateDateTime = Utility.Now;
        }
    }
}
