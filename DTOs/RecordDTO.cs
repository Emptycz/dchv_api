
namespace dchv_api.DTOs;

public class RecordDTO : BaseDTO 
{
    public uint? ID { get; set; }
    public uint? PersonID { get; set; }
    public PersonDTO? Author { get; set; }
    public string Name { get; set; }
    public IFormFile File { get; set; }
    // public virtual ICollection<TableColumn>? Columns { get; set; }
    // public virtual ICollection<TableData>? Values { get; set; }
    // public virtual ICollection<TableGroup>? Group { get; set; }

}
