namespace dchv_api.DTOs;

public class TableColumnDTO : BaseDTO
{
    public uint ID { get; set; }
    public int Position { get; set; }
    public string Name { get; set; } = String.Empty;
    public virtual ICollection<TableDataDTO>? Values { get; set; } = new List<TableDataDTO>();
}
