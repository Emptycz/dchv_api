namespace dchv_api.DataRepositories;

public interface IBaseRepository<T>
{
    T? Get(T entity);
    Task<T?> GetAsync(T entity);
    IEnumerable<T?> GetAll();
    Task<IEnumerable<T?>> GetAllAsync();
    T Add(T entity);
    IEnumerable<T> Add(IEnumerable<T> entity);
    Task<T> AddAsync(T entity);
    Task<IEnumerable<T>> AddAsync(IEnumerable<T> entity);
    T? Update(T entity);
    IEnumerable<T?> Update(IEnumerable<T> entity);
    Task<T?> UpdateAsync(T entity);
    Task<IEnumerable<T?>> UpdateAsync(IEnumerable<T> entity);
    bool Delete(T entity);
    Task<bool> DeleteAsync(T entity);
}
