namespace dchv_api.Models;

public class TableColumn : BaseModel
{
    public uint ID { get; set; }
    public int Position { get; set; }
    public string Name { get; set; }
    public virtual ICollection<TableData>? Values { get; set; } = new List<TableData>();
}
