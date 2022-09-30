using Domain.SeedWork;
using Resources.Messages;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using static Domain.SeedWork.Constants;

namespace Domain.JobAgg
{
    public class Job :
        Entity,
        IEntityIdIsSetable,
        IEntityHasIsActive,
        IEntityHasIsSystemic,
        IEntityHasIsDeletable,
        IEntityHasIsUndeletable,
        IEntityHasUpdateDateTime
    {
        public Job()
        {
            IsActive = true;
            IsDeletable = false;
            IsVerified = false;
            Score = 0;
        }

        // **********
        public bool IsActive { get; set; }
        // **********

        // **********
        public bool IsDeletable { get; set; }
        // **********

        // **********
        public bool IsSystemic { get; set; }
        // **********

        // **********
        public bool IsUndeletable { get; set; }
        // **********

        // **********
        public bool IsVerified { get; set; }
        // **********

        // **********
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public DateTime InsertedDateTime { get; private set; }
        // **********

        // **********
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public DateTime UpdateDateTime { get; private set; }
        // **********

        public void SetId(Guid id)
        {
            Id = id;
        }

        public void SetUpdateDateTime()
        {
            UpdateDateTime = Utility.Now;
        }

        // **********
        [MaxLength
        (length: Constants.MaxLength.Name_Job,
        ErrorMessageResourceType = typeof(Validations),
        ErrorMessageResourceName = nameof(Validations.MaxLength))]

        [Display(
        ResourceType = typeof(Resources.DataDictionary),
        Name = nameof(Resources.DataDictionary.JobName))]
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
        public string? Description { get; set; }
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
        public string? Address { get; set; }
        // **********

        // **********
        [MaxLength
        (length: FixedLength.CellPhoneNumber,
        ErrorMessageResourceType = typeof(Validations),
        ErrorMessageResourceName = nameof(Validations.MaxLength))]

        [Display(
        ResourceType = typeof(Resources.DataDictionary),
        Name = nameof(Resources.DataDictionary.CellPhoneNumber))]
        public string? PhoneNumber1 { get; set; }
        // **********

        // **********
        [MaxLength
        (length: FixedLength.CellPhoneNumber,
        ErrorMessageResourceType = typeof(Validations),
        ErrorMessageResourceName = nameof(Validations.MaxLength))]

        [Display(
        ResourceType = typeof(Resources.DataDictionary),
        Name = nameof(Resources.DataDictionary.CellPhoneNumber))]
        public string? PhoneNumber2 { get; set; }
        // **********

        // **********
        [MaxLength
        (length: FixedLength.CellPhoneNumber,
        ErrorMessageResourceType = typeof(Validations),
        ErrorMessageResourceName = nameof(Validations.MaxLength))]

        [Display(
        ResourceType = typeof(Resources.DataDictionary),
        Name = nameof(Resources.DataDictionary.CellPhoneNumber))]
        public string? CellPhoneNumber1 { get; set; }
        // **********

        // **********
        [MaxLength
        (length: FixedLength.CellPhoneNumber,
        ErrorMessageResourceType = typeof(Validations),
        ErrorMessageResourceName = nameof(Validations.MaxLength))]

        [Display(
        ResourceType = typeof(Resources.DataDictionary),
        Name = nameof(Resources.DataDictionary.CellPhoneNumber))]
        public string? CellPhoneNumber2 { get; set; }
        // **********

        // **********
        public decimal? Ing { get; set; }
        // **********

        // **********
        public decimal? Lat { get; set; }
        // **********

        // **********
        public string? Tag { get; set; }
        // **********

        // **********
        [Display(
        ResourceType = typeof(Resources.DataDictionary),
        Name = nameof(Resources.DataDictionary.OpeningTime))]
        public DateTime OpeningTime { get; set; }
        // **********

        // **********
        [Display(
        ResourceType = typeof(Resources.DataDictionary),
        Name = nameof(Resources.DataDictionary.ClosingTime))]
        public DateTime ClosingTime { get; set; }
        // **********

        // **********
        public Guid CategoryId { get; set; }
        // **********

        // **********
        [Display(
        ResourceType = typeof(Resources.DataDictionary),
        Name = nameof(Resources.DataDictionary.Score))]
        public decimal Score { get; set; }
        // **********

        // **********
        public Guid OwnerId { get; set; }
        // **********
    }
}
