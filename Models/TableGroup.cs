using System.Collections.Specialized;
using System.ComponentModel.DataAnnotations;

namespace dchv_api.Models
{
    public class TableGroup : BaseModel
    {
        public int ID { get; set; }
        public string? Name { get; set; }
        [Range(1, int.MaxValue)]
        public int AuthorID { get; set; }
        public Person? Author { get; set; }
        public virtual ICollection<TableMeta>? Tables { get; set; }
    }
}