using Domain;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace Persistence.Repositories
{
    public class RepositoryBase<Tkey, T> : IRepository<Tkey, T> where T : class
    {
        public DbContext _Context { get; set; }
        public RepositoryBase(DbContext context)
        {
            _Context = context;
        }

        public void Create(T entity)
        {
            _Context.Add(entity);

        }
        public async Task CreateAsync(T entity)
        {
            await _Context.AddAsync(entity);
        }

        public bool Exist(Expression<Func<T, bool>> expresion)
        {
            return _Context.Set<T>().Any(expresion);

        }

        public T? Get(Tkey key)
        {
            return _Context.Find<T>(key);
        }

        public async Task<T?> GetAsync(Tkey key)
        {
            return await _Context.FindAsync<T>(key);
        }


        public void Remove(T entity)
        {
            _Context.Remove<T>(entity);
        }


        public void SaveChanges()
        {
            _Context.SaveChanges();
        }


        public async Task SaveChangesAsync()
        {
            await _Context.SaveChangesAsync();
        }

        public List<T> GetAll()
        {
            return _Context.Set<T>().ToList();
        }

        public async Task<List<T>> GetAllAsync()
        {
            return await _Context.Set<T>().ToListAsync();
        }

    }
}
