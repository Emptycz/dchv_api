using System.ComponentModel.DataAnnotations;
using dchv_api.Enums;

namespace dchv_api.Models;

public class Role : BaseModel
{
    [Key]
    public int ID { get; set; }
    public RolesEnum Slug { get; set; }
    public string Name { get; set; } = String.Empty;

    public virtual ICollection<Person>? Persons { get; set; }
}
