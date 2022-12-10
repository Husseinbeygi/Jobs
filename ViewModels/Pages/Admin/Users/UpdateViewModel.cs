using System.ComponentModel.DataAnnotations;
using Resources;

namespace ViewModels.Pages.Admin.Users;

public class UpdateViewModel : CommonViewModel
{
	public UpdateViewModel() : base()
	{
	}

	// **********
	[Display
		(Name = nameof(DataDictionary.Id),
		ResourceType = typeof(DataDictionary))]
	public Guid? Id { get; set; }
	// **********

	// **********
	[Display
		(ResourceType = typeof(DataDictionary),
		Name = nameof(DataDictionary.Role))]
	public string? Role { get; init; }
	// **********
}
