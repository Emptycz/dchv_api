using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace dchv_api.Models;

public class Record : BaseModel
{
    [Key]
    public uint ID { get; set; }
    public uint PersonID { get; set; }
    public string Name { get; set; } = String.Empty;

    [ForeignKey("PersonID")]
    public Person? Person { get; set; }
    public virtual ICollection<RecordData>? Data { get; set; }
    public virtual ICollection<TableGroup>? Group { get; set; }
}
