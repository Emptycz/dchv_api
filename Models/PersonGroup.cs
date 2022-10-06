using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace dchv_api.Models;

public class PersonGroup : BaseModel
{
    public uint ID { get; set; }
    public string Name { get; set; }
    public string? DisplayName { get; set; }
    [Range(1, uint.MaxValue)]
    public uint AuthorID { get; set; }

    [ForeignKey("AuthorID")]
    public Person? Author { get; set; }
    public virtual ICollection<Person>? Members { get; set; }
}
