namespace dchv_api.DataRepositories;

public interface IQueryableRepository<T, R>
{
    IEnumerable<T?> GetAll(R? filter);
    Task<IEnumerable<T>> GetAllAsync(R? filter);
}