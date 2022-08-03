namespace dchv_api.Models
{
    public class TableColumn : BaseModel
    {
        public int ID { get; set; }
        public int Position { get; set; }
        public string Name { get; set; }
        public virtual IList<TableData>? Values { get; set; } = new List<TableData>();
    }
}