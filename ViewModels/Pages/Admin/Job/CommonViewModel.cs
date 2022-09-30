using Domain.SeedWork;
using Resources.Messages;
using System.ComponentModel.DataAnnotations;
using static Domain.SeedWork.Constants;

namespace ViewModels.Pages.Admin.Job
{
    public class CommonViewModel
    {
        public CommonViewModel() : base()
        {

        }

        // **********
        [MaxLength
        (length: Constants.MaxLength.Name_Job,
        ErrorMessageResourceType = typeof(Validations),
        ErrorMessageResourceName = nameof(Validations.MaxLength))]

        [Display(
        ResourceType = typeof(Resources.DataDictionary),
        Name = nameof(Resources.DataDictionary.JobName))]

        [Required
        (ErrorMessageResourceType = typeof(Validations),
        ErrorMessageResourceName = nameof(Validations.Required))]
        public string Name { get; set; }
        // **********

        // **********
        [MaxLength
        (length: Constants.MaxLength.Description,
        ErrorMessageResourceType = typeof(Validations),
        ErrorMessageResourceName = nameof(Validations.MaxLength))]

        [Display(
        ResourceType = typeof(Resources.DataDictionary),
        Name = nameof(Resources.DataDictionary.Description))]

        [Required
        (ErrorMessageResourceType = typeof(Validations),
        ErrorMessageResourceName = nameof(Validations.Required))]
        public string Description { get; set; }
        // **********

        // **********
        [MaxLength
        (length: Constants.MaxLength.Address,
        ErrorMessageResourceType = typeof(Validations),
        ErrorMessageResourceName = nameof(Validations.MaxLength))]

        [Display(
        ResourceType = typeof(Resources.DataDictionary),
        Name = nameof(Resources.DataDictionary.Address))]

        [Required
        (ErrorMessageResourceType = typeof(Validations),
        ErrorMessageResourceName = nameof(Validations.Required))]
        public string Address { get; set; }
        // **********

        // **********
        [MaxLength
        (length: FixedLength.CellPhoneNumber,
        ErrorMessageResourceType = typeof(Validations),
        ErrorMessageResourceName = nameof(Validations.MaxLength))]

        [Display(
        ResourceType = typeof(Resources.DataDictionary),
        Name = nameof(Resources.DataDictionary.CellPhoneNumber))]

        [Required
        (ErrorMessageResourceType = typeof(Validations),
        ErrorMessageResourceName = nameof(Validations.Required))]
        public string PhoneNumber1 { get; set; }
        // **********

        // **********
        [Display(
        ResourceType = typeof(Resources.DataDictionary),
        Name = nameof(Resources.DataDictionary.OpeningTime))]

        [Required
        (ErrorMessageResourceType = typeof(Validations),
        ErrorMessageResourceName = nameof(Validations.Required))]
        public DateTime OpeningTime { get; set; }
        // **********

        // **********
        [Display(
        ResourceType = typeof(Resources.DataDictionary),
        Name = nameof(Resources.DataDictionary.ClosingTime))]

        [Required
        (ErrorMessageResourceType = typeof(Validations),
        ErrorMessageResourceName = nameof(Validations.Required))]
        public DateTime ClosingTime { get; set; }
        // **********

        // **********
        [Display
        (Name = nameof(Resources.DataDictionary.IsActive),
        ResourceType = typeof(Resources.DataDictionary))]
        public bool IsActive { get; set; }
        // **********

        // **********
        [Display
        (Name = nameof(Resources.DataDictionary.IsDeleted),
        ResourceType = typeof(Resources.DataDictionary))]
        public bool IsDeletable { get; set; }
        // **********

        // **********
        [Display
        (Name = nameof(Resources.DataDictionary.IsUndeletable),
        ResourceType = typeof(Resources.DataDictionary))]
        public bool IsUndeletable { get; set; }
        // **********

        // **********
        [Display
        (Name = nameof(Resources.DataDictionary.IsVerified),
        ResourceType = typeof(Resources.DataDictionary))]
        public bool IsVerified { get; set; }
        // **********
    }
}
