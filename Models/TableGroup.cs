namespace dchv_api.Models
{
    public class TableGroup : BaseModel
    {
        public int ID { get; set; }
        public string? Name { get; set; }
        public Person Author { get; set; }
        public virtual IEnumerable<TableMeta> Tables { get; set; }
    }
}