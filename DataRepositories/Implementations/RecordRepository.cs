using System.IO.Enumeration;
using System.Text;
using dchv_api.Database;
using dchv_api.DataHelpers;
using dchv_api.Models;
using dchv_api.RequestModels;
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
      _context.Add<Record>(entity);
      _context.SaveChanges();
      return this.Get(entity)!;
    }

    public IEnumerable<Record> Add(IEnumerable<Record> entity)
    {
      throw new NotImplementedException();
    }

    public async Task<Record> AddAsync(Record entity)
    {
      await _context.AddAsync<Record>(entity);
      await _context.SaveChangesAsync();
      return entity;
    }

    public Task<IEnumerable<Record>> AddAsync(IEnumerable<Record> entity)
    {
      throw new NotImplementedException();
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
      return _context.Record.Where(
        (x) => x.ID == entity.ID && x.Deleted_at == null
      )
      .Include(record => record.Person)
      .Include(record => record.Data)
      .SingleOrDefault();
    }

    public IEnumerable<Record?> GetAll(RecordRequest? filter)
    {
      var ctx = _context.Record.AsQueryable();

      if (!String.IsNullOrEmpty(filter?.Name))
      {
        ctx = ctx.Where((x) => x.Name.Contains(filter.Name));
      }

      if (filter?.PersonID > 0)
      {
        ctx = ctx.Where((x) => x.PersonID == filter.PersonID);
      }

      ctx = QueryableHelper<Record>.ApplyQuery(ctx, filter);

      return ctx
        .Where((x) => x.Deleted_at == null)
        .Include(record => record.Person)
        .OrderByDescending((x) => x.ID);
    }

    public IEnumerable<Record?> GetAll()
    {
      return _context.Record
        .Where((x) => x.Deleted_at == null)
        .Include(record => record.Person)
        .OrderByDescending((x) => x.ID).ToArray();
    }

    public async Task<IEnumerable<Record>> GetAllAsync(RecordRequest? filter)
    {
      var ctx = _context.Record.AsQueryable();

      if (!String.IsNullOrEmpty(filter?.Name))
      {
        ctx = ctx.Where((x) => x.Name.Contains(filter.Name));
      }

      if (filter?.PersonID > 0)
      {
        ctx = ctx.Where((x) => x.PersonID == filter.PersonID);
      }

      ctx = QueryableHelper<Record>.ApplyQuery(ctx, filter);

      return await ctx
        .Where((x) => x.Deleted_at == null)
        .Include(record => record.Person)
        .OrderByDescending((x) => x.ID).ToArrayAsync();
    }

    public async Task<IEnumerable<Record?>> GetAllAsync()
    {
      return await _context.Record
        .Where((x) => x.Deleted_at == null)
        .Include(record => record.Person)
        .OrderByDescending((x) => x.ID).ToArrayAsync();    }

    public async Task<Record?> GetAsync(Record entity)
    {
      return await _context.Record.Where(
        (x) => x.ID == entity.ID && x.Deleted_at == null
      )
      .Include(record => record.Person)
      .Include(record => record.Data)
      .SingleOrDefaultAsync();
    }

    public Record? Update(Record entity)
    {
      throw new NotImplementedException();
    }

    public IEnumerable<Record?> Update(IEnumerable<Record> entity)
    {
      throw new NotImplementedException();
    }

    public Task<Record?> UpdateAsync(Record entity)
    {
      throw new NotImplementedException();
    }

    public Task<IEnumerable<Record?>> UpdateAsync(IEnumerable<Record> entity)
    {
      throw new NotImplementedException();
    }
  }
}