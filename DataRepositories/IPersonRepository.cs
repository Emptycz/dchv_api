using dchv_api.Models;

namespace dchv_api.DataRepositories;

public interface IPersonRepository : IBaseRepository<Person>
{
    IEnumerable<Person> GetByLoginId(uint id);
}
