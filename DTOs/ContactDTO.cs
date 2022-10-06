namespace dchv_api.DTOs;

public class ContactDTO : BaseDTO
{
    public int ID { get; set; }
    public ContactTypeDTO Type { get; set; }
    public string Value { get; set; }
    public PersonDTO Person { get; set; }
}
