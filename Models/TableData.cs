using System.ComponentModel.DataAnnotations;

namespace dchv_api.Models
{
    public class TableData : BaseModel
    {
        public int ID { get; set; }
        [Range(1, int.MaxValue)]
        public int TableColumnID { get; set; }
        public TableColumn? Column { get; set; }
        public string Value { get; set; }
        [Range(1, int.MaxValue)]
        public int ListKey { get; set; }
        public string? ListName { get; set; }
    }
}