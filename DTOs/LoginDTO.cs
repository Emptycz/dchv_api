using dchv_api.Models;

namespace dchv_api.DTOs;

public class LoginDTO : BaseDTO
{
    public uint ID { get; set; }
    public string Username { get; set; } = String.Empty;
    public DateTime? Verified_at { get; set; }

    public IEnumerable<PersonDTO>? Persons { get; set; }

}

