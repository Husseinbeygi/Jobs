using Domain.SeedWork;
using Resources.Messages;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data;
using static Domain.SeedWork.Constants;

namespace Domain.UserAgg;

public class User :
	Entity,
	IEntityIdIsSetable,
	IEntityHasIsActive,
	IEntityHasIsSystemic,
	IEntityHasIsUndeletable,
	IEntityHasUpdateDateTime
{
	#region Static(s)
	public static readonly Guid
		SuperUserId = new(g: "CC75D635-EF6D-4E86-907A-BC532CDC3ACC");
	#endregion /Static(s)

	public User(string emailAddress) : base()
	{
		//SetUpdateDateTime();
		UpdateDateTime = InsertDateTime;

		//RoleId = roleId;
		EmailAddress = emailAddress;
		EmailAddressVerificationKey = Guid.NewGuid();

	}


	// **********
	[System.ComponentModel.DataAnnotations.Display
		(ResourceType = typeof(Resources.DataDictionary),
		Name = nameof(Resources.DataDictionary.Role))]

	public string? Role { get; set; }
	// **********

	//// **********
	//[System.ComponentModel.DataAnnotations.Display
	//	(ResourceType = typeof(Resources.DataDictionary),
	//	Name = nameof(Resources.DataDictionary.Role))]
	//public Guid? RoleId { get; set; }
	//// **********

	//// **********
	//[System.ComponentModel.DataAnnotations.Display
	//	(ResourceType = typeof(Resources.DataDictionary),
	//	Name = nameof(Resources.DataDictionary.Role))]

	//public virtual Role? Role { get; set; }
	//// **********

	// **********
	public bool IsActive { get; set; }
	// **********

	// **********
	public bool IsSystemic { get; set; }
	// **********

	// **********
	public bool IsProgrammer { get; set; }
	// **********

	// **********
	public bool IsUndeletable { get; set; }
	// **********

	// **********
	public bool IsProfilePublic { get; set; }
	// **********

	// **********
	public bool IsEmailAddressVerified { get; set; }
	// **********

	// **********
	public bool IsCellPhoneNumberVerified { get; set; }
	// **********

	// **********
	[DatabaseGenerated(DatabaseGeneratedOption.None)]

	public DateTime UpdateDateTime { get; private set; }
	// **********

	// **********
	[MaxLength
		(length: Constants.MaxLength.Username,
		ErrorMessageResourceType = typeof(Validations),
		ErrorMessageResourceName = nameof(Validations.MaxLength))]

	public string? Username { get; set; }
	// **********

	// **********
	[MinLength
		(length: Constants.FixedLength.DatabasePassword,
		ErrorMessageResourceType = typeof(Validations),
		ErrorMessageResourceName = nameof(Validations.MinLength))]

	[MaxLength
		(length: Constants.FixedLength.DatabasePassword,
		ErrorMessageResourceType = typeof(Validations),
		ErrorMessageResourceName = nameof(Validations.MaxLength))]

	public string? Password { get; set; }
	// **********

	// **********
	[Display
		(ResourceType = typeof(Resources.DataDictionary),
		Name = nameof(Resources.DataDictionary.FullName))]

	[MaxLength
		(length: Constants.MaxLength.FullName,
		ErrorMessageResourceType = typeof(Validations),
		ErrorMessageResourceName = nameof(Validations.MaxLength))]
	public string? FullName { get; set; }
	// **********

	// **********
	[Required
		(ErrorMessageResourceType = typeof(Validations),
		ErrorMessageResourceName = nameof(Validations.Required))]

	[MaxLength
		(length: MaxLength.EmailAddress,
		ErrorMessageResourceType = typeof(Validations),
		ErrorMessageResourceName = nameof(Validations.MaxLength))]

	[RegularExpression
		(pattern: RegularExpression.EmailAddress,
		ErrorMessageResourceType = typeof(Validations),
		ErrorMessageResourceName = nameof(Validations.EmailAddress))]
	public string EmailAddress { get; set; }
	// **********

	// **********
	public Guid EmailAddressVerificationKey { get; private set; }
	// **********

	// **********
	[MaxLength
		(length: FixedLength.CellPhoneNumber,
		ErrorMessageResourceType = typeof(Validations),
		ErrorMessageResourceName = nameof(Validations.MaxLength))]

	[RegularExpression
		(pattern: RegularExpression.CellPhoneNumber,
		ErrorMessageResourceType = typeof(Validations),
		ErrorMessageResourceName = nameof(Validations.CellPhoneNumber))]
	public string? CellPhoneNumber { get; set; }
	// **********

	// **********
	[Display
		(ResourceType = typeof(Resources.DataDictionary),
		Name = nameof(Resources.DataDictionary.CellPhoneNumberVerificationKey))]

	[MinLength
		(length: Constants.MinLength.CellPhoneNumberVerificationKey,
		ErrorMessageResourceType = typeof(Validations),
		ErrorMessageResourceName = nameof(Validations.MinLength))]

	[MaxLength
		(length: Constants.MaxLength.CellPhoneNumberVerificationKey,
		ErrorMessageResourceType = typeof(Validations),
		ErrorMessageResourceName = nameof(Validations.MaxLength))]
	public string? CellPhoneNumberVerificationKey { get; private set; }
	// **********

	// **********
	public string? Description { get; set; }
	// **********

	// **********
	public string? AdminDescription { get; set; }
	// **********

	public void SetUpdateDateTime()
	{
		UpdateDateTime =
			Utility.Now;
	}

	public void SetId(Guid id)
	{
		Id = id;
	}

}
