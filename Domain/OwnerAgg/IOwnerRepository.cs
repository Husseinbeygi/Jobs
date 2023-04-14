namespace Domain.OwnerAgg
{
    public interface IOwnerRepository : IRepository<Guid, Owner>
    {
        Task<IList<Owner>> GetAllAsync();

        Task<Owner> GetByName(string lname);
    }
}
