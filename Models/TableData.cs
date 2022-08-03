namespace dchv_api.Models
{
    public class TableData : BaseModel
    {
        public int ID { get; set; }
        public TableColumn Column { get; set; }
        public string Value { get; set; }
        public int ListKey { get; set; }
        public string? ListName { get; set; }
    }
}