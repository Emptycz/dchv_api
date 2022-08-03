namespace dchv_api.Models
{
    public class Role : BaseModel
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public string? DisplayName { get; set; }
        public Person Author { get; set; }
        public IEnumerable<Person>? Persons { get; set; }
}
}