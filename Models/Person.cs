namespace dchv_api.Models 
{
    public class Person : BaseModel
    {
        public int ID { get; set; }
        public int LoginID { get; set; }
        public string Firstname { get; set; }
        public string Lastname { get; set; }
        public string Email { get; set; }
        public virtual IEnumerable<Role> Roles { get; set; }
        public virtual IEnumerable<Contact> Contacts { get; set; }
    }
}