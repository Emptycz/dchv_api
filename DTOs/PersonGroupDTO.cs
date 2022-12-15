namespace dchv_api.DTOs;

public class PersonGroupDTO : BaseDTO
{
    public uint ID { get; set; }
    public string Name { get; set; } = String.Empty;
    public string? DisplayName { get; set; }

    public uint? PersonID { get; set; }
    public virtual PersonDTO? Person { get; set; }
    public virtual ICollection<PersonGroupRelationsDTO>? Members { get; set; }

}