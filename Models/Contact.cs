using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace dchv_api.Models;

public class Contact : BaseModel
{
    [Key]
    public uint ID { get; set; }

    [Range(1, uint.MaxValue)]
    public uint ContactTypeID { get; set; }
    
    [Range(1, uint.MaxValue)]
    public uint PersonID { get; set; }
    public string Value { get; set; } = String.Empty;
    
    [ForeignKey("PersonID")]
    public Person? Person { get; set; }
    
    [ForeignKey("ContactTypeID")]
    public ContactType? Type { get; set; }

}

