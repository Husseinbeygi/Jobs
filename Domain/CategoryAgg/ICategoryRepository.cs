namespace Domain.CategoryAgg
{
    public interface ICategoryRepository : IRepository<Guid, Category>
    {
        Task<Category> GetByName(string categoryName);
        Task<List<Category>> GetChildsById(Guid parentId);
        Task<List<Category>> GetParents();
    }
}
