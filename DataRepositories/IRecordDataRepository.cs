using dchv_api.Models;

namespace dchv_api.DataRepositories;

public interface IRecordDataRepository : IBaseRepository<RecordData>
{
  IEnumerable<RecordData>? GetAll(RecordData filter);
}