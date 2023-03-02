using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace dchv_api.Models;

public enum PersonGroupRelationState
{
    [Description("banned")]
    banned = 0,

    [Description("waiting")]
    waiting = 1,

    [Description("joined")]
    joined = 2,
}

public class PersonGroupRelations : BaseModel
{
    [Key]
    public uint ID { get; set; }
    public uint PersonID { get; set; }
    public uint PersonGroupID { get; set; }
    public PersonGroupRelationState State { get; set; } = PersonGroupRelationState.waiting;

    [ForeignKey("PersonID")]
    public Person? Person { get; set; }
    [ForeignKey("PersonGroupID")]
    public PersonGroup? Group { get; set; }
}