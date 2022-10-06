using dchv_api.Models;

namespace dchv_api.DTOs;

public class PersonDTO : BaseDTO
{
    public uint ID { get; set; }
    public uint LoginID { get; set; }
    public string Firstname { get; set; } = String.Empty;
    public string Lastname { get; set; } = String.Empty;

    public IEnumerable<RoleDTO>? Roles { get; set; }
    public IEnumerable<ContactDTO>? Contacts { get; set; }

}
