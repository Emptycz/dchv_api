using dchv_api.Enums;

namespace dchv_api.DTOs;

public class RoleDTO : BaseDTO
{
    public int ID { get; set; }
    public RolesEnum Slug { get; set; }
    public string Name { get; set; } = String.Empty;
    public string? DisplayName { get; set; }
    public uint PersonID { get; set; }
    public PersonDTO? Author { get; set; }
}
