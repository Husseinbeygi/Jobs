using Resources.Messages;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using static Domain.SeedWork.Constants;

namespace ViewModels.Pages.Admin.Job
{
    public class DetailsViewModel : UpdateViewModel
    {
        // ********
        [Display
        (Name = nameof(Resources.DataDictionary.Id),
         ResourceType = typeof(Resources.DataDictionary))]
        public Guid? Id { get; set; }
        // ********

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
        [Display(
        ResourceType = typeof(Resources.DataDictionary),
        Name = nameof(Resources.DataDictionary.Score))]
        public decimal Score { get; set; }
        // **********

        // **********
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public DateTime InsertedDateTime { get; private set; }
        // **********

        // **********
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public DateTime UpdateDateTime { get; private set; }
        // **********
    }
}
