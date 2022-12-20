using dchv_api.Database.Models;

namespace dchv_api.RequestModels;

public sealed class PersonRequest : QueryableRequest
{
    public uint? LoginID { get; set; }
    public string? Firstname { get; set; } = String.Empty;
    public string? Lastname { get; set; } = String.Empty;

}