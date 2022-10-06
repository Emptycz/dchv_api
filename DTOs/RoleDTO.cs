namespace dchv_api.DTOs;

public class RoleDTO : BaseDTO
{
    public int ID { get; set; }
    public string Name { get; set; }
    public string? DisplayName { get; set; }
    public uint AuthorID { get; set; }
    public PersonDTO? Author { get; set; }
    public IEnumerable<PersonDTO>? Persons { get; set; }
}
