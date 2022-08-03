namespace dchv_api.DataRepositories
{
    public interface IBaseRepository<T>
    {
        T Get(T entity);
        Task<T> GetAsync(T entity);
        IEnumerable<T> GetAll();
        Task<IEnumerable<T>> GetAllAsync();
        T Add(T entity);
        Task<T> AddAsync(T entity);
        T Update(T entity);
        Task<T> UpdateAsync(T entity);
        bool Delete(T entity);
        Task<bool> DeleteAsync(T entity);
    }
}