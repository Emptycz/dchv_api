using dchv_api.Models;
using dchv_api.Database;
using Microsoft.EntityFrameworkCore;

namespace dchv_api.DataRepositories.Implementations;

public class PersonGroupRepository : IPersonGroupRepository
{
    private readonly BaseDbContext _context;
    private readonly ILogger<PersonGroupRepository> _logger;
    public PersonGroupRepository(
        ILogger<PersonGroupRepository> logger,
        BaseDbContext dbContext
    )
    {
        _logger = logger;
        _context = dbContext;
    }

    public PersonGroup Add(PersonGroup entity)
    {
        _context.Add(entity);
        _context.SaveChanges();
        if (entity.ID != 0) return entity;
        throw new InvalidDataException("New person_group record was not created");
    }

  public IEnumerable<PersonGroup> Add(IEnumerable<PersonGroup> entity)
  {
    throw new NotImplementedException();
  }

  public Task<PersonGroup> AddAsync(PersonGroup entity)
  {
    throw new NotImplementedException();
  }

  public Task<IEnumerable<PersonGroup>> AddAsync(IEnumerable<PersonGroup> entity)
  {
    throw new NotImplementedException();
  }

  public bool Delete(PersonGroup entity)
    {
        entity = this.Get(entity)!;
        if (entity is null) return false;
        DateTime now = DateTime.UtcNow;
        entity.Deleted_at = now;
        if (entity.Members is not null) {
            foreach (PersonGroupRelations rel in entity.Members)
            {
                rel.Deleted_at = now;
            }
        }
        _context.Update<PersonGroup>(entity);
        return _context.SaveChanges() > 0 ? true : false;
    }

  public Task<bool> DeleteAsync(PersonGroup entity)
  {
    throw new NotImplementedException();
  }

  public PersonGroup? Get(PersonGroup entity)
    {
        return _context.PersonGroup
            .Include(pg => pg.Members)
            .ThenInclude(x => x.Person)
            .Where((x) => (
                x.ID == entity.ID &&
                x.Deleted_at == null)
            ).SingleOrDefault();
    }

    public IEnumerable<PersonGroup>? GetAll()
    {
        return _context.PersonGroup
            .Include(pg => pg.Person)
            .Where((x) => x.Deleted_at == null)
            .OrderByDescending((x) => x.Created_at);
    }

  public IEnumerable<PersonGroup>? GetAll(PersonGroup? filter)
  {
    throw new NotImplementedException();
  }

  public Task<IEnumerable<PersonGroup>>? GetAllAsync()
    {
      throw new NotImplementedException();
    }

  public Task<IEnumerable<PersonGroup>>? GetAllAsync(PersonGroup? filter)
  {
    throw new NotImplementedException();
  }

  public Task<PersonGroup>? GetAsync(PersonGroup entity)
    {
      throw new NotImplementedException();
    }

    public PersonGroup? Update(PersonGroup entity)
    {
        _context.Update(entity);
        _context.SaveChanges();
        return this.Get(entity);
    }

  public IEnumerable<PersonGroup>? Update(IEnumerable<PersonGroup> entity)
  {
    throw new NotImplementedException();
  }

  public Task<PersonGroup>? UpdateAsync(PersonGroup entity)
  {
    throw new NotImplementedException();
  }

  public Task<IEnumerable<PersonGroup>>? UpdateAsync(IEnumerable<PersonGroup> entity)
  {
    throw new NotImplementedException();
  }
}