using System.ComponentModel.DataAnnotations.Schema;

namespace dchv_api.Models;

public class PersonGroupRecordGroup : BaseModel
{
  public uint ID { get; set; }
  public uint PersonGroupID { get; set; }
  public uint RecordGroupID { get; set; }

  [ForeignKey("PersonGroupID")]
  public virtual PersonGroup? PersonGroup { get; set; }

  [ForeignKey("RecordGroupID")]
  public virtual RecordGroup? RecordGroup { get; set; }
}
