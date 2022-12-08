using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.UserAgg;

namespace ViewModels.Pages.Admin.Comments;
public class CreateViewModel : object
{
	public CreateViewModel() : base()
	{
	}

	// **********
	[System.ComponentModel.DataAnnotations.Display
		(Name = nameof(Resources.DataDictionary.Comments),
		ResourceType = typeof(Resources.DataDictionary))]
	public string Comment { get; set; }
	// **********

	// **********
	[System.ComponentModel.DataAnnotations.Display
		(Name = nameof(Resources.DataDictionary.Score),
		ResourceType = typeof(Resources.DataDictionary))]
	public decimal Score { get; set; }
	// **********

	// **********
	[System.ComponentModel.DataAnnotations.Display
		(Name = nameof(Resources.DataDictionary.UserId),
		ResourceType = typeof(Resources.DataDictionary))]
	public Guid UserId { get; set; }
	// **********

	// **********
	[System.ComponentModel.DataAnnotations.Display
		(Name = nameof(Resources.DataDictionary.OwnerId),
		ResourceType = typeof(Resources.DataDictionary))]
	public Guid OwnerId { get; set; }
	// **********
}
