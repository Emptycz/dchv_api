namespace dchv_api.Models
{
    public class TableMeta : BaseModel
    {
        public int ID { get; set; }
        public Person Author { get; set; }
        public virtual IEnumerable<TableColumn>? Columns { get; set; }
        public virtual IEnumerable<TableData>? Values { get; set; }
        public virtual IEnumerable<TableGroup>? Group { get; set; }
    }
}