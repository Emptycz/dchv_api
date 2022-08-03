namespace dchv_api.DTOs
{
    public class BaseDTO
    {
        public DateTime Created_at { get; set; }
        public DateTime? Modified_at { get; set; }
        public DateTime? Deleted_at { get; set; }

    }
}