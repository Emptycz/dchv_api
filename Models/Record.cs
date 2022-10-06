using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace dchv_api.Models;

public class Record : BaseModel
{
    public uint ID { get; set; }
    public uint AuthorID { get; set; }
    [ForeignKey("AuthorID")]
    public Person? Author { get; set; }
    public string Name { get; set; }
    public virtual ICollection<TableColumn>? Columns { get; set; }
    public virtual ICollection<TableData>? Values { get; set; }
    public virtual ICollection<TableGroup>? Group { get; set; }
}
