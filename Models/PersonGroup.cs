using System.ComponentModel.DataAnnotations.Schema;

namespace dchv_api.Models;

public class PersonGroup : BaseModel
{
    public uint ID { get; set; }
    public string Name { get; set; } = String.Empty;
    public string? DisplayName { get; set; }
    public uint PersonID { get; set; }

    [ForeignKey("PersonID")]
    public virtual Person? Person { get; set; }
    public virtual ICollection<PersonGroupRelations>? Members { get; set; }
}
