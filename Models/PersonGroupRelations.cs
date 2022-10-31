using System.ComponentModel.DataAnnotations.Schema;

namespace dchv_api.Models;

public class PersonGroupRelations : BaseModel
{
  public uint PersonID { get; set; }
  public uint PersonGroupID { get; set; }

  [ForeignKey("PersonID")]
  public Person? Person { get; set; }
  [ForeignKey("PersonGroupID")]
  public PersonGroup? Group { get; set; }
}