namespace dchv_api.Models
{
    public class Role : BaseModel
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public string? DisplayName { get; set; }
        // FIXME: This double person Relationship causes trouble,
        // fix it manually (define the relation manually in DatabaseContext.cs)
        
        // public Person Author { get; set; }
        public ICollection<Person>? Persons { get; set; }
    }
}