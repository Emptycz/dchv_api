using dchv_api.Models;
using dchv_api.RequestModels;

namespace dchv_api.DataRepositories;

public interface IPersonGroupRelationsRepository : IBaseRepository<PersonGroupRelations>, IQueryableRepository<PersonGroupRelations, PersonGroupRelationsRequest>
{
}
