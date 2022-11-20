using dchv_api.Models;
using dchv_api.Database;
using dchv_api.DTOs;
using AutoMapper;
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
        return _context.Logins.Where(
            (x) => x.ID == entity.ID && x.Deleted_at == null
        ).Single();
    }

    public Login? LoginUser(Login entity)
    {
       return _context.Logins
            ?.Include(login => login.Persons)
            ?.ThenInclude((person) => person.Roles)
            .AsSplitQuery()
            .Where((x) =>
                x.Username == entity.Username &&
                x.Password == entity.Password &&
                x.Deleted_at == null
            ).SingleOrDefault();
    }

    public Task<Login> AddAsync(Login entity)
    {
        throw new NotImplementedException();
    }

    public Login? Get(Login entity)
    {
        return _context.Logins
            ?.Include(login => login.Persons)
            ?.ThenInclude((person) => person.Roles)
            ?.Include(login => login.Persons)
            ?.ThenInclude((person) => person.Contacts)
            .AsSplitQuery()
            .Where(
                (x) => x.ID == entity.ID && x.Deleted_at == null
            ).SingleOrDefault();
    }

    public IEnumerable<Login>? GetAll()
    {
        return _context.Logins
            ?.Include(login => login.Persons)
            ?.ThenInclude((person) => person.Roles)
            .AsSplitQuery()
            .Where((x) => x.Deleted_at == null);
    }

    public Task<IEnumerable<Login>> GetAllAsync()
    {
        throw new NotImplementedException();
    }

    public Task<Login> GetAsync(Login entity)
    {
        throw new NotImplementedException();
    }

    public bool Delete(Login entity)
    {
        entity = this.Get(entity)!;
        if (entity is null) return false;
        entity.Deleted_at = DateTime.UtcNow;
        _context.Update<Login>(entity);
        return _context.SaveChanges() > 0 ? true : false;
    }

    public Task<bool> DeleteAsync(Login entity)
    {
        throw new NotImplementedException();
    }

    public Login Update(Login entity)
    {
        throw new NotImplementedException();
    }

    public Task<Login> UpdateAsync(Login entity)
    {
        throw new NotImplementedException();
    }
}
