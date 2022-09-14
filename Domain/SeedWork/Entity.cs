using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.SeedWork
{
	public abstract class Entity : object, IEntity<Guid>
	{
		public Entity() : base()
		{
			Ordering = 10_000;

			InsertDateTime =
				Framework.Utility.Now;

			Id =
				System.Guid.NewGuid();
		}

		// **********
		[DatabaseGenerated
			(DatabaseGeneratedOption.None)]
		public System.Guid Id { get; protected set; }
		// **********

		// **********
		[Range
			(minimum: 1, maximum: 100_000,
			ErrorMessageResourceType = typeof(Resources.Messages.Validations),
			ErrorMessageResourceName = nameof(Resources.Messages.Validations.Range))]
		public int Ordering { get; set; }
		// **********

		// **********
		[Display
			(ResourceType = typeof(Resources.DataDictionary),
			Name = nameof(Resources.DataDictionary.InsertDateTime))]

		[DatabaseGenerated
			(DatabaseGeneratedOption.None)]
		public System.DateTime InsertDateTime { get; private set; }
		// **********
	}
}
