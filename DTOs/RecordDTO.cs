
namespace dchv_api.DTOs;

public class RecordDTO : BaseDTO
{
    public uint? ID { get; set; }
    public uint? PersonID { get; set; }
    public string Name { get; set; } = String.Empty;

    public PersonDTO? Person { get; set; }
    public ICollection<RecordDataDTO>? Data { get; set; }
    // public virtual ICollection<TableColumnDTO>? Columns { get; set; }
    // public virtual ICollection<TableData>? Values { get; set; }
    // public virtual ICollection<TableGroup>? Group { get; set; }

}
