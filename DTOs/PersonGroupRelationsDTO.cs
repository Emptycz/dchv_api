using dchv_api.Models;

namespace dchv_api.DTOs;

public class PersonGroupRelationsDTO : BaseDTO
{
  public uint ID { get; set; }
  public uint PersonID { get; set; }
  public uint PersonGroupID { get; set; }
  public PersonGroupRelationState State { get; set; }

  public virtual PersonDTO? Person { get; set; }
  public virtual PersonGroupDTO? Group { get; set; }
}