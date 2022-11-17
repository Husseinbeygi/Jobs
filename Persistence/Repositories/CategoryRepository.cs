using Domain.CategoryAgg;
using Microsoft.EntityFrameworkCore;

namespace Persistence.Repositories
{
    public class CategoryRepository : RepositoryBase<Guid, Category>, ICategoryRepository
    {
        private readonly DatabaseContext _context;

        public CategoryRepository(DatabaseContext context) : base(context)
        {
            _context = context;
        }

        public async Task<Category> GetByName(string categoryName)
        {
            return await _context.Categories.FirstOrDefaultAsync(x => x.Name == categoryName);
        }

        public async Task<List<Category>> GetChildsById(Guid parentId)
        {
            return await _context.Categories.Where(x => x.ParentId == parentId).OrderBy(x => x.Ordering).ToListAsync();
        }

        public async Task<List<Category>> GetParents()
        {
            return await _context.Categories.Where(x => x.ParentId == null).OrderBy(x => x.Ordering).ToListAsync();
        }
    }
}
