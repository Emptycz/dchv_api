using dchv_api.DTOs;

public class RecordCanvasDTO
{
  public List<RecordDTO> Records { get; set; } = new List<RecordDTO>();
  public List<RecordGroupDTO> Groups { get; set; } = new List<RecordGroupDTO>();
}