using dchv_api.Models;
using dchv_api.RequestModels;

namespace dchv_api.DataRepositories;

public interface IPersonRepository : IBaseRepository<Person>, IQueryableRepository<Person, PersonRequest>
{
    IEnumerable<Person> GetByLoginId(uint id);
}
