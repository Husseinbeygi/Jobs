using System.Linq.Expressions;

namespace Domain
{
	public interface IRepository<TKey, T> where T : class
	{

		T? Get(TKey key);
		List<T> GetAll();
		void SaveChanges();
		void Create(T entity);
		Task SaveChangesAsync();
		Task CreateAsync(T entity);
		Task<T?> GetAsync(TKey key);
		Task<List<T>> GetAllAsync();
		bool Exist(Expression<Func<T, bool>> expresion);
		void Remove(T entity);
	}
}
