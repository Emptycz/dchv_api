using System.ComponentModel.DataAnnotations.Schema;

namespace dchv_api.Models;

public class BaseModel
{
    public DateTime Created_at { get; set; } = DateTime.UtcNow;
    public DateTime? Modified_at { get; set; }
    public DateTime? Deleted_at { get; set; }
}
