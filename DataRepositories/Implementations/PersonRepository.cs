using dchv_api.Models;
using dchv_api.Database;
using dchv_api.RequestModels;
using Microsoft.EntityFrameworkCore;
using dchv_api.DataHelpers;

namespace dchv_api.DataRepositories.Implementations;

public class PersonRepository : IPersonRepository
{
    private readonly BaseDbContext _context;
    private readonly ILogger<PersonRepository> _logger;
    public PersonRepository(
        ILogger<PersonRepository> logger,
        BaseDbContext dbContext
    )
    {
        _logger = logger;
        _context = dbContext;
    }
    public Person Add(Person entity)
    {
        _context.Add(entity);
        _context.SaveChanges();
        if (entity.ID != 0) return entity;
        throw new InvalidDataException("New person record was not created");
    }

    public Person? Get(Person entity)
    {
        return _context.Person.Where((x) => (
            x.ID == entity.ID &&
            x.Deleted_at == null))
                .SingleOrDefault();
    }

    public IEnumerable<Person?> GetAll()
    {
        return _context.Person.Where((x) => x.Deleted_at == null);
    }

    public bool Delete(Person entity)
    {
        entity = this.Get(entity)!;
        if (entity is null) return false;
        entity.Deleted_at = DateTime.UtcNow;
        _context.Update<Person>(entity);
        return _context.SaveChanges() > 0 ? true : false;
    }

    public IEnumerable<Person> GetByLoginId(uint id)
    {
        return _context.Person.Where((x) => x.LoginID == id && x.Deleted_at == null);
    }

    public async Task<Person?> GetAsync(Person entity)
    {
        return await _context.Person.Where((x) => (
            x.ID == entity.ID &&
            x.Deleted_at == null))
                .SingleOrDefaultAsync();
    }

    public async Task<IEnumerable<Person?>> GetAllAsync()
    {
        return await _context.Person.Where((x) => x.Deleted_at == null).ToArrayAsync();
    }

    public IEnumerable<Person> Add(IEnumerable<Person> entity)
    {
      throw new NotImplementedException();
    }

    public async Task<Person> AddAsync(Person entity)
    {
        await _context.AddAsync(entity);
        await _context.SaveChangesAsync();
        if (entity.ID != 0) return entity;
        throw new InvalidDataException("New person record was not created");
    }

    public Task<IEnumerable<Person>> AddAsync(IEnumerable<Person> entity)
    {
      throw new NotImplementedException();
    }

    public Person? Update(Person entity)
    {
      throw new NotImplementedException();
    }

    public IEnumerable<Person?> Update(IEnumerable<Person> entity)
    {
      throw new NotImplementedException();
    }

    public Task<Person?> UpdateAsync(Person entity)
    {
      throw new NotImplementedException();
    }

    public Task<IEnumerable<Person?>> UpdateAsync(IEnumerable<Person> entity)
    {
      throw new NotImplementedException();
    }

    public Task<bool> DeleteAsync(Person entity)
    {
      throw new NotImplementedException();
    }

    public IEnumerable<Person?> GetAll(PersonRequest? filter)
    {
      var ctx = _context.Person.AsQueryable();

      ctx = _prepareFilteredQuery(ctx, filter);
      ctx = QueryableHelper<Person>.ApplyQuery(ctx, filter);

      return ctx
        .Where((x) => x.Deleted_at == null)
        .OrderByDescending((x) => x.ID);
    }

    public async Task<IEnumerable<Person>> GetAllAsync(PersonRequest? filter)
    {
      var ctx = _context.Person.AsQueryable();

      ctx = _prepareFilteredQuery(ctx, filter);
      ctx = QueryableHelper<Person>.ApplyQuery(ctx, filter);

      return await ctx
        .Where((x) => x.Deleted_at == null)
        .OrderByDescending((x) => x.ID).ToArrayAsync();
    }

    private IQueryable<Person> _prepareFilteredQuery(IQueryable<Person> ctx, PersonRequest? filter)
    {
      if (!String.IsNullOrEmpty(filter?.Firstname))
      {
        ctx = ctx.Where((x) => x.Firstname.Contains(filter.Firstname));
      }

      if (!String.IsNullOrEmpty(filter?.Lastname))
      {
        ctx = ctx.Where((x) => x.Lastname.Contains(filter.Lastname));
      }

      if (filter?.LoginID > 0)
      {
        ctx = ctx.Where((x) => x.LoginID == filter.LoginID);
      }
      return ctx;
    }
}
