using dchv_api.Database.Models;
using dchv_api.DTOs;

namespace dchv_api.RequestModels;

public class RecordRequest : QueryableRequest
{
    public string? Name { get; set; }
    public int? PersonID { get; set; }
    public IEnumerable<RecordDataRequest>? Data { get; set; }
}

public class RecordDataRequest : QueryableRequest
{
    public int? Row { get; set; }
    public int? Column { get; set; }
    public string? Value { get; set; }
}