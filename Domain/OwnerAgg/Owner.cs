using Domain.SeedWork;
using Resources.Messages;
using System.ComponentModel.DataAnnotations;

namespace Domain.OwnerAgg
{
    public class Owner : Entity,
    IEntityIdIsSetable,
    IEntityHasIsActive,
    IEntityHasIsSystemic,
    IEntityHasIsUndeletable,
    IEntityHasUpdateDateTime

    {
        // **********
        public bool IsActive { get; set; }
        // **********

        // **********
        public bool IsSystemic { get; set; }
        // **********

        // **********
        public bool IsUndeletable { get; set; }
        // **********

        // **********
        public DateTime UpdateDateTime { get; private set; }
        // **********

        public void SetId(Guid id)
        {
            Id = id;
        }

        public void SetUpdateDateTime()
        {
            UpdateDateTime =
                Utility.Now;
        }

        // **********
        [MaxLength
        (length: Constants.MaxLength.FullName,
        ErrorMessageResourceType = typeof(Validations),
        ErrorMessageResourceName = nameof(Validations.MaxLength))]

        [Display
        (ResourceType = typeof(Resources.DataDictionary),
        Name = nameof(Resources.DataDictionary.FullName))]

        public string FName { get; set; }
        // **********

        // **********
        [MaxLength
        (length: Constants.MaxLength.LastName,
        ErrorMessageResourceType = typeof(Validations),
        ErrorMessageResourceName = nameof(Validations.MaxLength))]

        [Display
        (ResourceType = typeof(Resources.DataDictionary),
        Name = nameof(Resources.DataDictionary.LastName))]

        public string LName { get; set; }
        // **********

        // **********
        [MaxLength
        (length: Constants.FixedLength.CellPhoneNumber,
        ErrorMessageResourceType = typeof(Validations),
        ErrorMessageResourceName = nameof(Validations.MaxLength))]

        [Display
        (ResourceType = typeof(Resources.DataDictionary),
        Name = nameof(Resources.DataDictionary.CellPhoneNumber))]

        public string? PhoneNumber { get; set; }
        // **********

        // **********
        [Display
        (ResourceType = typeof(Resources.DataDictionary),
        Name = nameof(Resources.DataDictionary.Jobs))]

        public List<Guid> JobId { get; set; }
        // **********
    }
}
