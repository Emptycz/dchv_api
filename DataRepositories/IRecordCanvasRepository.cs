using dchv_api.Models;
using dchv_api.RequestModels;

namespace dchv_api.DataRepositories;

public interface IRecordCanvasRepository
{
  RecordCanvasDTO? GetRootDirectoryContent();
  RecordCanvasDTO? GetDirectoryContent(uint RecordGroupID);

  RecordCanvasDTO? GetRootDirectoryContent(RecordCanvasRequest? filter);
  RecordCanvasDTO? GetDirectoryContent(uint RecordGroupID, RecordCanvasRequest? filter);

}