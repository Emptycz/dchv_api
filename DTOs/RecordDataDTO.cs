namespace dchv_api.DTOs;

public class RecordDataDTO : BaseDTO
{
    public uint ID { get; set; }
    public uint Row { get; set; }
    public uint Column { get; set; }
    public string Value { get; set; } = String.Empty;
    public uint RecordID { get; set; }
}