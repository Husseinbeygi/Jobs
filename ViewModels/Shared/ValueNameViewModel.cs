using Resources;
using System.ComponentModel.DataAnnotations;

namespace ViewModels.Shared
{
    public class ValueNameViewModel
    {
		// **********
		[Display
			(ResourceType = typeof(DataDictionary),
			Name = nameof(DataDictionary.Id))]
		public string? Value { get; set; }
		// **********

		// **********
		[Display
			(ResourceType = typeof(DataDictionary),
			Name = nameof(DataDictionary.Name))]
		public string? Name { get; set; }
		// **********
	}
}
