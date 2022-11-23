using dchv_api.Models;

namespace dchv_api.DataRepositories;

public interface IRecordRepository : IBaseRepository<Record>
{
  IEnumerable<Record>? GetAll(Record filter);
}
