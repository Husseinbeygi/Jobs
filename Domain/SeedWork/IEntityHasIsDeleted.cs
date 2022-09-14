namespace Domain.SeedWork
{
	public interface IEntityHasIsDeleted
	{
		bool IsDeleted { get; set; }

		DateTime DeleteDateTime { get; }

		void SetDeleteDateTime();
	}
}
