namespace ViewModels.Pages.Admin.Comments;

public class DetailsViewModel : VerifyViewModel
{
	public DetailsViewModel() : base()
	{
	}

	// **********
	[System.ComponentModel.DataAnnotations.Display
		(Name = nameof(Resources.DataDictionary.Id),
		ResourceType = typeof(Resources.DataDictionary))]
	public System.Guid? Id { get; init; }
	// **********

	// **********
	[System.ComponentModel.DataAnnotations.Display
		(Name = nameof(Resources.DataDictionary.Comments),
		ResourceType = typeof(Resources.DataDictionary))]
	public string Comment { get; init; }
	// **********

	[System.ComponentModel.DataAnnotations.Display
		(Name = nameof(Resources.DataDictionary.Score),
		ResourceType = typeof(Resources.DataDictionary))]
	public decimal Score { get; init; }

	// **********
	[System.ComponentModel.DataAnnotations.Display
		(Name = nameof(Resources.DataDictionary.IsVerified),
		ResourceType = typeof(Resources.DataDictionary))]
	public bool IsVerified { get; init; }
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
