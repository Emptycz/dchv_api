using System.ComponentModel.DataAnnotations;

namespace dchv_api.Models
{
    public class Contact : BaseModel
    {
        public int ID { get; set; }
        [Range(1, int.MaxValue)]
        public int ContactTypeID { get; set; }
        [Range(1, int.MaxValue)]
        public int PersonID { get; set; }
        public string Value { get; set; }
        public Person? Person { get; set; }
        public ContactType? Type { get; set; }
    
    }

}