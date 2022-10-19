using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace dchv_api.Models;

public class Record : BaseModel
{
    [Key]
    public uint ID { get; set; }
    public uint AuthorID { get; set; }
    public string Name { get; set; } = String.Empty;
    // public virtual ICollection<TableColumn>? Columns { get; set; }
    // public virtual ICollection<TableData>? Values { get; set; }
    [ForeignKey("AuthorID")]
    public Person? Author { get; set; }
    public virtual ICollection<RecordData>? Data { get; set; }
    public virtual ICollection<TableGroup>? Group { get; set; }
}
