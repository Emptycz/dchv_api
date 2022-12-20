using dchv_api.Database.Models;

namespace dchv_api.RequestModels;

public class LoginRequest : QueryableRequest
{
    public string? Name { get; set; }
    public string? Username { get; set; } = String.Empty;
    public DateTime? Verified_at { get; set; }
}