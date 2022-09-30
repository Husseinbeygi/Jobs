using Domain.JobAgg;
using Domain.UserAgg;
using Microsoft.EntityFrameworkCore;

namespace Persistence.Repositories
{
    public class JobRepository : RepositoryBase<Guid, Job>, IJobRepository
    {
        private readonly DatabaseContext _context;

        public JobRepository(DatabaseContext context) : base(context)
        {
            _context = context;
        }

        public async Task<IList<Job>> GetAllAsync()
        {
            return await _context.Jobs.ToListAsync();

        }

        public async Task<Job> GetByName(string name)
        {
            return await _context.Jobs.FirstOrDefaultAsync(x => x.Name == name);
        }
    }
}
