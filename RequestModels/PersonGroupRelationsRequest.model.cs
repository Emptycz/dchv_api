using dchv_api.Database.Models;
using dchv_api.Models;

namespace dchv_api.RequestModels;

public sealed class PersonGroupRelationsRequest : QueryableRequest
{
    public uint? PersonID { get; set; }
    public PersonGroupRelationState? State { get; set; }
    public Boolean? IsAuthor { get; set; }
}