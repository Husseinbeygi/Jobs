namespace Domain.JobAgg
{
    public interface IJobRepository : IRepository<Guid, Job>
    {
        Task<IList<Job>> GetAllAsync();

        Task<Job> GetByName(string Name);
    }
}
