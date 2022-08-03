namespace dchv_api.Models {
    public class Login : BaseModel {
        public int ID { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public DateTime? Verified_at { get; set; }
        public virtual IEnumerable<Person>? Persons { get; set; }
    }
}