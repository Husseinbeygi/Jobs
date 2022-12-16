namespace ViewModels.Pages.Admin.Users;

public class DetailsViewModel : UpdateViewModel
{
	public DetailsViewModel() : base()
	{
	}


	// **********
	[System.ComponentModel.DataAnnotations.Display
		(ResourceType = typeof(Resources.DataDictionary),
		Name = nameof(Resources.DataDictionary.IsActive))]
	public bool? IsRoleActive { get; init; }
	// **********

	// **********
	[System.ComponentModel.DataAnnotations.Display
		(ResourceType = typeof(Resources.DataDictionary),
		Name = nameof(Resources.DataDictionary.IsProfilePublic))]
	public bool IsProfilePublic { get; init; }
	// **********

	// **********
	[System.ComponentModel.DataAnnotations.Display
		(ResourceType = typeof(Resources.DataDictionary),
		Name = nameof(Resources.DataDictionary.IsEmailAddressVerified))]
	public bool IsEmailAddressVerified { get; init; }
	// **********

	// **********
	[System.ComponentModel.DataAnnotations.Display
		(ResourceType = typeof(Resources.DataDictionary),
		Name = nameof(Resources.DataDictionary.IsCellPhoneNumberVerified))]
	public bool IsCellPhoneNumberVerified { get; init; }
	// **********

	// **********
	[System.ComponentModel.DataAnnotations.Display
		(Name = nameof(Resources.DataDictionary.IsSystemic),
		ResourceType = typeof(Resources.DataDictionary))]
	public bool IsSystemic { get; init; }
	// **********

	// **********
	[System.ComponentModel.DataAnnotations.Display
		(Name = nameof(Resources.DataDictionary.UpdateDateTime),
		ResourceType = typeof(Resources.DataDictionary))]
	public System.DateTime? UpdateDateTime { get; init; }
	// **********

	// **********
	[System.ComponentModel.DataAnnotations.Display
		(Name = nameof(Resources.DataDictionary.InsertDateTime),
		ResourceType = typeof(Resources.DataDictionary))]
	public System.DateTime? InsertDateTime { get; init; }
	// **********
}

