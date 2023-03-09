using dchv_api.Database.Models;

namespace dchv_api.RequestModels;

public sealed class RecordCanvasRequest : QueryableRequest
{
    public uint? PersonID { get; set; }
    public Boolean? Shared { get; set; }
}