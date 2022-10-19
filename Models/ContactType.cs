using System.ComponentModel.DataAnnotations;

namespace dchv_api.Models;

public class ContactType : BaseModel
{
    [Key]
    public uint ID { get; set; }
    public string Name { get; set; } = String.Empty;
}
