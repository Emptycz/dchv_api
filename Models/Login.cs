using System.ComponentModel.DataAnnotations;

namespace dchv_api.Models;

public class Login : BaseModel {
    [Key]
    public uint ID { get; set; }
    public string Username { get; set; } = String.Empty;
    public string Password { get; set; } = String.Empty;
    public DateTime? Verified_at { get; set; }

    public virtual IEnumerable<Person>? Persons { get; set; }
}
