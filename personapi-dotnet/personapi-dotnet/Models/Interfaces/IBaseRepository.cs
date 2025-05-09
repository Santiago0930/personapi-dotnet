namespace personapi_dotnet.Interfaces
{
	public interface IBaseRepository<T> where T : class
	{
		Task<IEnumerable<T>> GetAllAsync();
		Task<T> GetByIdAsync(object id);
		Task AddAsync(T entity);
		void Update(T entity);
		void Delete(T entity);
		Task SaveAsync();
	}
}