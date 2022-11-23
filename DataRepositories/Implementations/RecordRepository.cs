using System.Text;
using dchv_api.Database;
using dchv_api.Models;
using Microsoft.EntityFrameworkCore;

namespace dchv_api.DataRepositories.Implementations
{
  public class RecordRepository : IRecordRepository
  {
    private readonly BaseDbContext _context;
    private readonly ILogger<RecordRepository> _logger;

    public RecordRepository(ILogger<RecordRepository> logger, BaseDbContext dbContext)
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
      return this.Get(entity)!;
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
      .Include(record => record.Person)
      .Include(record => record.Data)
      .SingleOrDefault();
    }

    public IEnumerable<Record>? GetAll() => throw new NotImplementedException();

    public IEnumerable<Record>? GetAll(Record filter)
    {
      var ctx = _context.Records.AsQueryable();

      if (!String.IsNullOrEmpty(filter.Name)) {
        ctx = ctx.Where((x) => x.Name.Contains(filter.Name));
      }

      return ctx
        .Where((x) => x.Deleted_at == null)
        .Include(record => record.Person)
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