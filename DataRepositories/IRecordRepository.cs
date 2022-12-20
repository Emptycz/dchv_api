using dchv_api.Models;
using dchv_api.RequestModels;

namespace dchv_api.DataRepositories;

public interface IRecordRepository : IBaseRepository<Record>, IQueryableRepository<Record, RecordRequest>
{ }
