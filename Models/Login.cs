namespace dchv_api.Models {
    public class Login : BaseModel {
        public int ID { get; set; }
        public string Username { get; set; } = String.Empty;
        public string Password { get; set; } = String.Empty;
        public DateTime? Verified_at { get; set; }
        public virtual ICollection<Person>? Persons { get; set; }
    }
}