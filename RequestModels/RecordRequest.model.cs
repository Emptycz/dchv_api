using dchv_api.Database.Models;

namespace dchv_api.RequestModels;

public class RecordRequest : QueryableRequest
{
    public string? Name { get; set; }
    public uint? PersonID { get; set; }
}