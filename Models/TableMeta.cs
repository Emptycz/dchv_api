using System.ComponentModel.DataAnnotations;

namespace dchv_api.Models
{
    public class TableMeta : BaseModel
    {
        public int ID { get; set; }
        [Range(1, int.MaxValue)]
        public int PersonID { get; set; }
        public Person? Author { get; set; }
        public virtual ICollection<TableColumn>? Columns { get; set; }
        public virtual ICollection<TableData>? Values { get; set; }
        public virtual ICollection<TableGroup>? Group { get; set; }
    }
}