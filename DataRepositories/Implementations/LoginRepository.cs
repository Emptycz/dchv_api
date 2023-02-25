using dchv_api.Models;
using dchv_api.Database;
using Microsoft.EntityFrameworkCore;

namespace dchv_api.DataRepositories.Implementations;

public class LoginRepository : ILoginRepository
{
    private readonly BaseDbContext _context;
    private readonly ILogger<LoginRepository> _logger;

    public LoginRepository(ILogger<LoginRepository> logger, BaseDbContext dbContext)
    {
        _logger = logger;
        _context = dbContext;
    }
    public Login Add(Login entity)
    {
        _context.Add(entity);
        _context.SaveChanges();
        return entity;
    }

    public Login? LoginUser(Login entity)
    {
       return _context.Login
            ?.Include(login => login.Persons)
            .ThenInclude((person) => person.Roles)
            .AsSplitQuery()
            .Where((x) =>
                x.Username == entity.Username &&
                x.Password == entity.Password &&
                x.Deleted_at == null
            ).SingleOrDefault();
    }

    public Login? Get(Login entity)
    {
        return _context.Login
            ?.Include(login => login.Persons)
            ?.ThenInclude((person) => person.Roles)
            ?.Include(login => login.Persons)
            ?.ThenInclude((person) => person.Contacts)
            .AsSplitQuery()
            .Where(
                (x) => x.ID == entity.ID && x.Deleted_at == null
            ).SingleOrDefault();
    }

    public bool Delete(Login entity)
    {
        entity = this.Get(entity)!;
        if (entity is null) return false;
        entity.Deleted_at = DateTime.UtcNow;
        _context.Update<Login>(entity);
        return _context.SaveChanges() > 0 ? true : false;
    }

    public IEnumerable<Login?>? GetAll(Login? filter = null)
    {
        return _context.Login
            ?.Include(login => login.Persons)
            ?.ThenInclude((person) => person.Roles)
            .Where((x) => x.Deleted_at == null).ToArray()
            .OrderByDescending((x) => x.ID);
    }

  public async Task<Login?> GetAsync(Login entity)
  {
      return await _context.Login
          .Include(login => login.Persons)
          .ThenInclude((person) => person.Roles)
          .Include(login => login.Persons)
          .ThenInclude((person) => person.Contacts)
          .Where(
              (x) => x.ID == entity.ID && x.Deleted_at == null
          ).SingleAsync();
  }

    public IEnumerable<Login?> GetAll()
    {
        return _context.Login
            ?.Include(login => login.Persons)
            ?.ThenInclude((person) => person.Roles)
            .Where((x) => x.Deleted_at == null)
            .OrderByDescending((x) => x.ID);
    }

  public async Task<IEnumerable<Login?>> GetAllAsync()
  {
        return await _context.Login
            .Include(login => login.Persons)
            .ThenInclude((person) => person.Roles)
            .Where((x) => x.Deleted_at == null).ToArrayAsync();
  }

  public IEnumerable<Login> Add(IEnumerable<Login> entity)
  {
    throw new NotImplementedException();
  }

  public Task<Login> AddAsync(Login entity)
  {
    throw new NotImplementedException();
  }

  public Task<IEnumerable<Login>> AddAsync(IEnumerable<Login> entity)
  {
    throw new NotImplementedException();
  }

  public Login? Update(Login entity)
  {
    throw new NotImplementedException();
  }

  public IEnumerable<Login?> Update(IEnumerable<Login> entity)
  {
    throw new NotImplementedException();
  }

  public Task<Login?> UpdateAsync(Login entity)
  {
    throw new NotImplementedException();
  }

  public Task<IEnumerable<Login?>> UpdateAsync(IEnumerable<Login> entity)
  {
    throw new NotImplementedException();
  }

  public Task<bool> DeleteAsync(Login entity)
  {
    throw new NotImplementedException();
  }
}
