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
        PersonGroup? res = this.Get(entity);
        if (!(res is null)) return res;
        throw new InvalidDataException("New person_group record was not created");
    }

    public bool Delete(PersonGroup entity)
    {
        entity = this.Get(entity)!;
        if (entity is null) return false;
        entity.Deleted_at = DateTime.UtcNow;
        _context.Update<PersonGroup>(entity);
        return _context.SaveChanges() > 0 ? true : false;
    }

    public PersonGroup? Get(PersonGroup entity)
    {
        return _context.PersonGroups
            .Include(pg => pg.Members)
            .ThenInclude(x => x.Person)
            .Where((x) => (
                x.ID == entity.ID &&
                x.Deleted_at == null)
            ).SingleOrDefault();
    }

    public IEnumerable<PersonGroup>? GetAll()
    {
        return _context.PersonGroups
            .Include(pg => pg.Person)
            .Where((x) => x.Deleted_at == null);
    }

    public Task<IEnumerable<PersonGroup>>? GetAllAsync()
    {
      throw new NotImplementedException();
    }

    public Task<PersonGroup>? GetAsync(PersonGroup entity)
    {
      throw new NotImplementedException();
    }

    public PersonGroup Update(PersonGroup entity)
    {
      throw new NotImplementedException();
    }
}