namespace dchv_api.DTOs
{
    public class LoginDTO : BaseDTO
    {
        public int ID { get; set; }
        public string Username { get; set; }
        public DateTime? Verified_at { get; set; }
    }
}