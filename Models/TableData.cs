using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace dchv_api.Models;

public class TableData : BaseModel
{
    [Key]
    public uint ID { get; set; }
    [Range(1, uint.MaxValue)]
    public uint TableColumnID { get; set; }
    public string Value { get; set; }
    [Range(1, int.MaxValue)]
    public int ListKey { get; set; }
    public string? ListName { get; set; }

    [ForeignKey("ColumnID")]
    public virtual TableColumn? Column { get; set; }
}
