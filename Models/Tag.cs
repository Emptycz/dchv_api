using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace dchv_api.Models;

public class Tag : BaseModel
{
    [Key]
    public uint ID { get; set; }
    public uint PersonID { get; set; }
    public string Name { get; set; } = String.Empty;

    [ForeignKey("PersonID")]
    public virtual Person? Person { get; set; } = null;
    public virtual ICollection<Record>? Record { get; set; }
}
