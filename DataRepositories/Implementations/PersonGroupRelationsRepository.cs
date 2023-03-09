using dchv_api.Models;
using dchv_api.Database;
using Microsoft.EntityFrameworkCore;
using dchv_api.RequestModels;
using dchv_api.DataHelpers;

namespace dchv_api.DataRepositories.Implementations;

public class PersonGroupRelationsRepository : IPersonGroupRelationsRepository
{
    private readonly BaseDbContext _context;
    private readonly ILogger<PersonGroupRelationsRepository> _logger;
    public PersonGroupRelationsRepository(
        ILogger<PersonGroupRelationsRepository> logger,
        BaseDbContext dbContext
    )
    {
        _logger = logger;
        _context = dbContext;
    }

    public PersonGroupRelations Add(PersonGroupRelations entity)
    {
      throw new NotImplementedException();
    }

    public IEnumerable<PersonGroupRelations> Add(IEnumerable<PersonGroupRelations> entity)
    {
      throw new NotImplementedException();
    }

    public Task<PersonGroupRelations> AddAsync(PersonGroupRelations entity)
    {
      throw new NotImplementedException();
    }

    public Task<IEnumerable<PersonGroupRelations>> AddAsync(IEnumerable<PersonGroupRelations> entity)
    {
      throw new NotImplementedException();
    }

    public bool Delete(PersonGroupRelations entity)
    {
      PersonGroupRelations? data = this.Get(new PersonGroupRelations { ID = entity.ID });
      if (data is null) return false;
      data.Deleted_at = DateTime.Now;
      _context.PersonGroupRelations.Update(data);
      return _context.SaveChanges() > 0;
    }

    public async Task<bool> DeleteAsync(PersonGroupRelations entity)
    {
      PersonGroupRelations? data = await this.GetAsync(new PersonGroupRelations { ID = entity.ID });
      if (data is null) return false;
      data.Deleted_at = DateTime.Now;
      _context.PersonGroupRelations.Update(data);
      return await _context.SaveChangesAsync() > 0;
    }

    public PersonGroupRelations? Get(PersonGroupRelations entity)
    {
      return _context.PersonGroupRelations
        .Where((x) => x.ID == entity.ID && x.Deleted_at == null)
        .SingleOrDefault();
    }

    public IEnumerable<PersonGroupRelations?> GetAll()
    {
        return _context.PersonGroupRelations
          .Where((x) => x.Deleted_at == null)
          .Include(x => x.Group).ToArray();
    }

    public IEnumerable<PersonGroupRelations?> GetAll(PersonGroupRelationsRequest? filter)
    {
      var ctx = _context.PersonGroupRelations.AsQueryable();

      ctx = _prepareFilteredQuery(ctx, filter);
      ctx = QueryableHelper<PersonGroupRelations>.ApplyQuery(ctx, filter);

      return _context.PersonGroupRelations
          .Where((x) => x.Deleted_at == null)
          .Include(x => x.Group).ToArray();
    }

    public async Task<IEnumerable<PersonGroupRelations?>> GetAllAsync()
    {
        return await _context.PersonGroupRelations
          .Where((x) => x.Deleted_at == null)
          .Include(x => x.Group).ToArrayAsync();
    }

    public async Task<IEnumerable<PersonGroupRelations>> GetAllAsync(PersonGroupRelationsRequest? filter)
    {
      var ctx = _context.PersonGroupRelations.AsQueryable();

      ctx = _prepareFilteredQuery(ctx, filter);
      ctx = QueryableHelper<PersonGroupRelations>.ApplyQuery(ctx, filter);

      return await ctx
          .Where((x) => x.Deleted_at == null)
          .Include(x => x.Group)
          .ThenInclude(x => x!.Person)
          .OrderByDescending(x => x.Created_at)
          .ToArrayAsync();
    }

    public async Task<PersonGroupRelations?> GetAsync(PersonGroupRelations entity)
    {
      return await _context.PersonGroupRelations
        .Where((x) => x.ID == entity.ID && x.Deleted_at == null)
        .SingleOrDefaultAsync();
    }

    public PersonGroupRelations? Update(PersonGroupRelations entity)
    {
      _context.PersonGroupRelations.Update(entity);
      _context.SaveChanges();
      return entity;
    }

    public IEnumerable<PersonGroupRelations?> Update(IEnumerable<PersonGroupRelations> entity)
    {
      throw new NotImplementedException();
    }

    public async Task<PersonGroupRelations?> UpdateAsync(PersonGroupRelations entity)
    {
      // _context.PersonGroupRelations.IgnoreQueryFilters();
      var ent = this.Get(entity);
      if (ent is null) throw new Exception();
      ent.State = entity.State;

      _context.PersonGroupRelations.Attach(ent);
      _context.PersonGroupRelations.Entry(ent).Property(e => e.State).IsModified = true;
      _context.PersonGroupRelations.Update(ent);
      await _context.SaveChangesAsync();
      return entity;
    }

    public Task<IEnumerable<PersonGroupRelations?>> UpdateAsync(IEnumerable<PersonGroupRelations> entity)
    {
      throw new NotImplementedException();
    }

    private IQueryable<PersonGroupRelations> _prepareFilteredQuery(IQueryable<PersonGroupRelations> ctx, PersonGroupRelationsRequest? filter)
    {
      if (filter?.State is not null)
      {
        // Ignore global filter in this usecase;
        ctx = ctx.IgnoreQueryFilters();
        ctx = ctx.Where((x) => x.State == filter.State);
      }

      if (filter?.IsAuthor is not null) {
        ctx = ctx.Include((x) => x.Group);
        if (filter.IsAuthor == false) {
          ctx = ctx.Where((x) => x.Group!.PersonID != x.PersonID);
        } else {
          ctx = ctx.Where((x) => x.Group!.PersonID == x.PersonID);
        }
      }

      if (filter?.PersonID is not null)
      {
        ctx = ctx.Where((x) => x.PersonID == filter.PersonID);
      }

      return ctx;
    }
}
