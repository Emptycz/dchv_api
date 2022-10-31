namespace dchv_api.DataRepositories;

public interface IBaseRepository<T>
{
    T? Get(T entity);
    Task<T>? GetAsync(T entity);
    IEnumerable<T>? GetAll();
    Task<IEnumerable<T>>? GetAllAsync();
    T Add(T entity);
    T Update(T entity);
    bool Delete(T entity);
}
