using dchv_api.Database;
using dchv_api.Models;
using Microsoft.EntityFrameworkCore;

namespace dchv_api.DataRepositories.Implementations
{
  public class RecordRepository : IRecordRepository
  {
    private readonly DatabaseContext _context;
    private readonly ILogger<RecordRepository> _logger;

    public RecordRepository(ILogger<RecordRepository> logger, DatabaseContext dbContext)
    {
        _logger = logger;
        _context = dbContext;
    }

    public Record Add(Record entity)
    {
      // FIXME: This is slow as hell, instead of saving thousands of lines,
      // you should aim for bulk insert, which is insanely faster (like 1000ms -> 6ms faster)
      // https://entityframeworkcore.com/saving-data-bulk-insert
      _context.Add(entity);
      _context.SaveChanges();
      return this.Get(entity);
    }

    public async Task<Record> AddAsync(Record entity)
    {
      await _context.AddAsync(entity);
      await _context.SaveChangesAsync();
      return _context.Records.Where(
        (x) => x.ID == entity.ID && x.Deleted_at == null
      ).Single();
    }

    public bool Delete(Record entity)
    {
      throw new NotImplementedException();
    }

    public Task<bool> DeleteAsync(Record entity)
    {
      throw new NotImplementedException();
    }

    public Record? Get(Record entity)
    {
      return _context.Records.Where(
        (x) => x.ID == entity.ID && x.Deleted_at == null
      )
      .Include(record => record.Author)
      .Include(record => record.Data)
      .SingleOrDefault();
    }

    public IEnumerable<Record>? GetAll()
    {
      return _context.Records
        .Where((x) => x.Deleted_at == null)
        .Include(record => record.Author)
        .OrderByDescending((x) => x.ID);
    }

    public Task<IEnumerable<Record>>? GetAllAsync()
    {
      throw new NotImplementedException();
    }

    public Task<Record>? GetAsync(Record entity)
    {
      throw new NotImplementedException();
    }

    public Record Update(Record entity)
    {
      throw new NotImplementedException();
    }

    public Task<Record> UpdateAsync(Record entity)
    {
      throw new NotImplementedException();
    }
  }
}