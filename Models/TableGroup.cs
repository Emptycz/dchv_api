using System.Collections.Specialized;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace dchv_api.Models;

public class TableGroup : BaseModel
{
    public uint ID { get; set; }
    public string? Name { get; set; }
    // [Range(1, int.MaxValue)]
    // public int AuthorID { get; set; }
    
    // [ForeignKey("AuthorID")]
    // public virtual Person? Author { get; set; }
    public virtual ICollection<Record>? Tables { get; set; }
}
