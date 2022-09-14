namespace Domain.SeedWork
{
	public interface IEntityHasUpdateDateTime
	{
		DateTime UpdateDateTime { get; }

		void SetUpdateDateTime();
	}
}
