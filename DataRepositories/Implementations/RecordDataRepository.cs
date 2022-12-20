using System.Runtime.InteropServices.ComTypes;
using dchv_api.Database;
using dchv_api.Models;

namespace dchv_api.DataRepositories.Implementations;

public class RecordDataRepository : IRecordDataRepository
{
    private readonly BaseDbContext _context;
    private readonly ILogger<RecordDataRepository> _logger;
    public RecordDataRepository(
        ILogger<RecordDataRepository> logger,
        BaseDbContext dbContext
    )
    {
        _logger = logger;
        _context = dbContext;
    }

  public RecordData Add(RecordData entity)
  {
    throw new NotImplementedException();
  }

  public IEnumerable<RecordData> Add(IEnumerable<RecordData> entity)
  {
    throw new NotImplementedException();
  }

  public Task<RecordData> AddAsync(RecordData entity)
  {
    throw new NotImplementedException();
  }

  public Task<IEnumerable<RecordData>> AddAsync(IEnumerable<RecordData> entity)
  {
    throw new NotImplementedException();
  }

  public bool Delete(RecordData entity)
  {
    throw new NotImplementedException();
  }

  public Task<bool> DeleteAsync(RecordData entity)
  {
    throw new NotImplementedException();
  }

  public RecordData? Get(RecordData entity)
  {
    throw new NotImplementedException();
  }

  public IEnumerable<RecordData>? GetAll()
  {
    return _context.RecordData.Where((x) => x.Deleted_at == null);
  }

  public IEnumerable<RecordData>? GetAll(RecordData? filter = null)
  {
    // TODO: Implement filter
    return _context.RecordData.Where((x) => x.Deleted_at == null);
  }

  public Task<IEnumerable<RecordData>>? GetAllAsync(RecordData? filter = null)
  {
    throw new NotImplementedException();
  }

  public Task<IEnumerable<RecordData>>? GetAllAsync()
  {
    throw new NotImplementedException();
  }

  public Task<RecordData>? GetAsync(RecordData entity)
  {
    throw new NotImplementedException();
  }

  public RecordData? Update(RecordData entity)
  {
    throw new NotImplementedException();
  }

  public IEnumerable<RecordData>? Update(IEnumerable<RecordData> entity)
  {
    // List<RecordData>? records = _context.RecordData.Where((x) => entity.Select((y) => y.ID).Contains(x.ID)).ToList();
    // if (records is null || records.Count() == 0) return new List<RecordData>();

    // for(int key = 0; key < records.Count(); key++)
    // {
    //   records[key].Modified_at = DateTime.UtcNow;
    //   var up = entity.Where((x) => x.ID == records[key].ID).SingleOrDefault();
    //   records[key].Value = up is null ? records[key].Value : up.Value;
    // }
    _context.RecordData.BulkUpdate(entity);
    _context.SaveChanges();
    return entity;
  }

  public Task<RecordData>? UpdateAsync(RecordData entity)
  {
    throw new NotImplementedException();
  }

  public Task<IEnumerable<RecordData>>? UpdateAsync(IEnumerable<RecordData> entity)
  {
    throw new NotImplementedException();
  }
}