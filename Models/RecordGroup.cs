using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace dchv_api.Models;

public class RecordGroup : BaseModel
{
    [Key]
    public uint ID { get; set; }
    public uint PersonID { get; set; }
    public uint? RecordGroupID { get; set; }
    public string? Name { get; set; }

    [ForeignKey("PersonID")]
    public virtual Person? Person { get; set; }
    [ForeignKey("RecordGroupID")]
    // public virtual RecordGroup? ParentGroup { get; set; }
    // [InverseProperty("ParentGroup")]
    public virtual ICollection<RecordGroup>? ChildGroups { get; set; }
    public virtual ICollection<Record>? Records { get; set; }
    public virtual ICollection<PersonGroupRecordGroup>? SharedPersonGroups { get; set; }
}
