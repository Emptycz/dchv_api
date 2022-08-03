namespace dchv_api.DTOs 
{
    public class PersonDTO : BaseDTO
    {
        public int ID { get; set; }
        public int LoginID { get; set; }
        public string Firstname { get; set; }
        public string Lastname { get; set; }
        public string Email { get; set; }
        public virtual IEnumerable<RoleDTO> Roles { get; set; }
        public virtual IEnumerable<ContactDTO> Contacts { get; set; }
    }
}