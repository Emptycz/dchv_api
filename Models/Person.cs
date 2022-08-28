using System.ComponentModel.DataAnnotations;

namespace dchv_api.Models 
{
    public class Person : BaseModel
    {
        public int ID { get; set; }
        public string Firstname { get; set; }
        public string Lastname { get; set; }
        [Range(1, int.MaxValue)]
        public int LoginID { get; set; }
        public Login? Login { get; set; }
        public virtual ICollection<Role>? Roles { get; set; }
        public virtual ICollection<Contact>? Contacts { get; set; }
    }
}