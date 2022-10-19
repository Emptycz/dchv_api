namespace dchv_api.DTOs;

public class TableDataDTO : BaseDTO
{
  public uint ID { get; set; }
  public uint TableColumnID { get; set; }
  public string Value { get; set; } = String.Empty;
  public int ListKey { get; set; }
  public string? ListName { get; set; }
}