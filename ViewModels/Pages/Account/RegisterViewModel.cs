namespace ViewModels.Pages.Account
{
	public class RegisterViewModel : object
	{
		public RegisterViewModel() : base()
		{
		}
        // **********
        [System.ComponentModel.DataAnnotations.Display
            (Name = nameof(Resources.DataDictionary.FirstName),
            ResourceType = typeof(Resources.DataDictionary))]

        [System.ComponentModel.DataAnnotations.Required
            (AllowEmptyStrings = false,
            ErrorMessageResourceType = typeof(Resources.Messages.Validations),
            ErrorMessageResourceName = nameof(Resources.Messages.Validations.Required))]

        [System.ComponentModel.DataAnnotations.MaxLength
            (length: Domain.SeedWork.Constants.MaxLength.FirstName,
            ErrorMessageResourceType = typeof(Resources.Messages.Validations),
            ErrorMessageResourceName = nameof(Resources.Messages.Validations.MaxLength))]

        public string FullName { get; set; }
        // **********


        // **********
        [System.ComponentModel.DataAnnotations.Display
			(Name = nameof(Resources.DataDictionary.Username),
			ResourceType = typeof(Resources.DataDictionary))]

		[System.ComponentModel.DataAnnotations.Required
			(AllowEmptyStrings = false,
			ErrorMessageResourceType = typeof(Resources.Messages.Validations),
			ErrorMessageResourceName = nameof(Resources.Messages.Validations.Required))]

		[System.ComponentModel.DataAnnotations.MaxLength
			(length: Domain.SeedWork.Constants.MaxLength.Username,
			ErrorMessageResourceType = typeof(Resources.Messages.Validations),
			ErrorMessageResourceName = nameof(Resources.Messages.Validations.MaxLength))]

		[System.ComponentModel.DataAnnotations.RegularExpression
			(pattern: Domain.SeedWork.Constants.RegularExpression.Username,
			ErrorMessageResourceType = typeof(Resources.Messages.Validations),
			ErrorMessageResourceName = nameof(Resources.Messages.Validations.Username))]
		public string? Username { get; set; }
		// **********

		// **********
		[System.ComponentModel.DataAnnotations.Display
			(Name = nameof(Resources.DataDictionary.Password),
			ResourceType = typeof(Resources.DataDictionary))]

		[System.ComponentModel.DataAnnotations.Required
			(AllowEmptyStrings = false,
			ErrorMessageResourceType = typeof(Resources.Messages.Validations),
			ErrorMessageResourceName = nameof(Resources.Messages.Validations.Required))]

		[System.ComponentModel.DataAnnotations.MaxLength
			(length: Domain.SeedWork.Constants.MaxLength.Password,
			ErrorMessageResourceType = typeof(Resources.Messages.Validations),
			ErrorMessageResourceName = nameof(Resources.Messages.Validations.MaxLength))]

		[System.ComponentModel.DataAnnotations.RegularExpression
			(pattern: Domain.SeedWork.Constants.RegularExpression.Password,
			ErrorMessageResourceType = typeof(Resources.Messages.Validations),
			ErrorMessageResourceName = nameof(Resources.Messages.Validations.Password))]

		[System.ComponentModel.DataAnnotations.DataType
			(dataType: System.ComponentModel.DataAnnotations.DataType.Password)]
		public string? Password { get; set; }
		// **********

		// **********
		[System.ComponentModel.DataAnnotations.Display
			(Name = nameof(Resources.DataDictionary.ConfirmPassword),
			ResourceType = typeof(Resources.DataDictionary))]

		[System.ComponentModel.DataAnnotations.Required
			(AllowEmptyStrings = false,
			ErrorMessageResourceType = typeof(Resources.Messages.Validations),
			ErrorMessageResourceName = nameof(Resources.Messages.Validations.Required))]

		//[System.ComponentModel.DataAnnotations.Compare
		//	(otherProperty: "Password",
		//	ErrorMessageResourceType = typeof(Resources.Messages.Validations),
		//	ErrorMessageResourceName = nameof(Resources.Messages.Validations.Compare))]

		[System.ComponentModel.DataAnnotations.Compare
			(otherProperty: nameof(Password),
			ErrorMessageResourceType = typeof(Resources.Messages.Validations),
			ErrorMessageResourceName = nameof(Resources.Messages.Validations.Compare))]

		[System.ComponentModel.DataAnnotations.DataType
			(dataType: System.ComponentModel.DataAnnotations.DataType.Password)]
		public string? ConfirmPassword { get; set; }
		// **********

		// **********
		[System.ComponentModel.DataAnnotations.Display
			(Name = nameof(Resources.DataDictionary.CellPhoneNumber),
			ResourceType = typeof(Resources.DataDictionary))]

		[System.ComponentModel.DataAnnotations.MaxLength
			(Domain.SeedWork.Constants.FixedLength.CellPhoneNumber,
			ErrorMessageResourceType = typeof(Resources.Messages.Validations),
			ErrorMessageResourceName = nameof(Resources.Messages.Validations.MaxLength))]

		[System.ComponentModel.DataAnnotations.RegularExpression
			(Domain.SeedWork.Constants.RegularExpression.CellPhoneNumber,
			ErrorMessageResourceType = typeof(Resources.Messages.Validations),
			ErrorMessageResourceName = nameof(Resources.Messages.Validations.CellPhoneNumber))]
		public string? CellPhoneNumber { get; set; }
		// **********

		// **********
		[System.ComponentModel.DataAnnotations.Display
			(Name = nameof(Resources.DataDictionary.EmailAddress),
			ResourceType = typeof(Resources.DataDictionary))]

		[System.ComponentModel.DataAnnotations.Required
			(AllowEmptyStrings = false,
			ErrorMessageResourceType = typeof(Resources.Messages.Validations),
			ErrorMessageResourceName = nameof(Resources.Messages.Validations.Required))]

		[System.ComponentModel.DataAnnotations.MaxLength
			(length: Domain.SeedWork.Constants.MaxLength.EmailAddress,
			ErrorMessageResourceType = typeof(Resources.Messages.Validations),
			ErrorMessageResourceName = nameof(Resources.Messages.Validations.MaxLength))]

		[System.ComponentModel.DataAnnotations.RegularExpression
			(pattern: Domain.SeedWork.Constants.RegularExpression.EmailAddress,
			ErrorMessageResourceType = typeof(Resources.Messages.Validations),
			ErrorMessageResourceName = nameof(Resources.Messages.Validations.EmailAddress))]
		public string? EmailAddress { get; set; }
		// **********
	}
}
