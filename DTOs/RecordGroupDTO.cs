using dchv_api.DTOs;

namespace dchv_api.DTOs;

public class RecordGroupDTO : BaseDTO
{
  public uint ID { get; set; }
  public uint PersonID { get; set; }
  public uint? RecordGroupID { get; set; }
  public string Name { get; set; } = String.Empty;


  public virtual PersonDTO? Person { get; set; }
  public virtual ICollection<RecordGroupDTO>? Groups { get; set; }
  public virtual ICollection<RecordDTO>? Records { get; set; }
}