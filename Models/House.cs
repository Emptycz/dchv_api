namespace dchv_api.Models
{
    public class House : BaseModel
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public string? DisplayName { get; set; }
        public Person Author { get; set; }
        public virtual IEnumerable<Person> Members { get; set; }
    }
}