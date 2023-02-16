using dchv_api.Models;
using dchv_api.RequestModels;

namespace dchv_api.DataRepositories;

public interface IRecordCanvasRepository
{
  RecordCanvasDTO? GetRootDirectoryContent();
  Task<RecordCanvasDTO>? GetRootDirectoryContentAsync();
  RecordCanvasDTO? GetDirectoryContent(uint RecordGroupID);
  Task<RecordCanvasDTO>? GetDirectoryContentAsync(uint RecordGroupID);
}