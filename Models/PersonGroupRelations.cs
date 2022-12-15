using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace dchv_api.Models;

public class PersonGroupRelations : BaseModel
{
  [Key]
  public uint ID { get; set; }
  public uint PersonID { get; set; }
  public uint PersonGroupID { get; set; }

  [ForeignKey("PersonID")]
  public Person? Person { get; set; }
  [ForeignKey("PersonGroupID")]
  public PersonGroup? Group { get; set; }
}