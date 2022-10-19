using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace dchv_api.Models;

public class RecordData : BaseModel
{
    [Key]
    public uint ID { get; set; }
    public uint Row { get; set; }
    public uint Column { get; set; }
    public string Value { get; set; } = String.Empty;
    public uint RecordID { get; set; }

    [ForeignKey("RecordID")]
    public virtual Record? Record { get; set; }

}