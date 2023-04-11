using Domain.OwnerAgg;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Persistence.Repositories
{
    public class OwnerRepository : RepositoryBase<Guid, Owner>, IOwnerRepository
    {
        private readonly DatabaseContext _context;

        public OwnerRepository(DatabaseContext context) : base(context)
        {
            _context = context;
        }

        public async Task<Owner> GetByName(string lname)
        {
            return await _context.Owners.FirstOrDefaultAsync(x => x.LName == lname);
        }

        public async Task<IList<Owner>> GetAllAsync()
        {
            return await _context.Owners.ToListAsync();
        }
    }
}
