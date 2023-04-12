using Domain.SeedWork;
using Resources.Messages;
using System.ComponentModel.DataAnnotations;

namespace ViewModels.Pages.Admin.Owner
{
    public class CommonViewModel
    {
        // **********
        [Display
        (ResourceType = typeof(Resources.DataDictionary),
        Name = nameof(Resources.DataDictionary.Id))]

        public Guid? Id { get; set; }
        // **********

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

        // **********
        [Display
        (ResourceType = typeof(Resources.DataDictionary),
        Name = nameof(Resources.DataDictionary.IsActive))]

        public bool IsActive { get; set; }
        // **********

        // **********
        [Display
        (ResourceType = typeof(Resources.DataDictionary),
        Name = nameof(Resources.DataDictionary.IsSystemic))]
        public bool IsSystemic { get; set; }
        // **********

        // **********
        [Display
        (ResourceType = typeof(Resources.DataDictionary),
        Name = nameof(Resources.DataDictionary.IsUndeletable))]

        public bool IsUndeletable { get; set; }
        // **********

        // **********
        [Display
        (ResourceType = typeof(Resources.DataDictionary),
        Name = nameof(Resources.DataDictionary.InsertDateTime))]

        public DateTime InsertDateTime { get; set; }
        // **********

        // **********
        [Display
        (ResourceType = typeof(Resources.DataDictionary),
        Name = nameof(Resources.DataDictionary.UpdateDateTime))]

        public DateTime UpdateDateTime { get; set; }
        // **********
    }
}
