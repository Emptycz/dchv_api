using System.ComponentModel.DataAnnotations.Schema;
using dchv_api.Enums;
using dchv_api.Extensions;

namespace dchv_api.Models;

public class Role : BaseModel
{
    public int ID { get; set; }
    public RolesEnum Slug { get; set; }
    public string Name { get; set; } = String.Empty;

    public virtual ICollection<Person>? Persons { get; set; }
}
