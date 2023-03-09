using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace dchv_api.Models;

public class Record : BaseModel
{
    [Key]
    public uint ID { get; set; }
    public uint PersonID { get; set; }
    public uint? RecordGroupID { get; set; }
    public string Name { get; set; } = String.Empty;

    [ForeignKey("PersonID")]
    public virtual Person? Person { get; set; } = null;
    [ForeignKey("RecordGroupID")]
    public virtual RecordGroup? RecordGroup { get; set; }
    public virtual ICollection<RecordData>? Data { get; set; }
    public virtual ICollection<PersonGroupRecord>? SharedPersonGroups { get; set; }
    // public virtual ICollection<Tag>? Tags { get; set; }
}
