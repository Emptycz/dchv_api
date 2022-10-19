using System.ComponentModel.DataAnnotations.Schema;

namespace dchv_api.Models;

public class Person : BaseModel
{
    public uint ID { get; set; }
    public string Firstname { get; set; } = String.Empty;
    public string Lastname { get; set; } = String.Empty;
    public uint LoginID { get; set; }

    [ForeignKey("LoginID")]
    public virtual Login? Login { get; set; }
    public virtual IEnumerable<Role>? Roles { get; set; }
    public virtual IEnumerable<Contact>? Contacts { get; set; }
}
