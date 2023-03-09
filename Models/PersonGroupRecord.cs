using System.ComponentModel.DataAnnotations.Schema;

namespace dchv_api.Models;

public class PersonGroupRecord : BaseModel
{
  public uint ID { get; set; }
  public uint PersonGroupID { get; set; }
  public uint RecordID { get; set; }

  [ForeignKey("PersonGroupID")]
  public virtual PersonGroup? PersonGroup { get; set; }

  [ForeignKey("RecordID")]
  public virtual Record? Record { get; set; }
}
