namespace dchv_api.Models
{
    public class Contact : BaseModel
    {
        public int ID { get; set; }
        public ContactType Type { get; set; }
        public string Value { get; set; }
        public Person Person { get; set; }
    }
}