using System.ComponentModel.DataAnnotations;

namespace dchv_api.Models
{
    public class House : BaseModel
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public string? DisplayName { get; set; }
        [Range(1, int.MaxValue)]
        public int AuthorID { get; set; }
        public Person? Author { get; set; }
        public virtual ICollection<Person>? Members { get; set; }
    }
}