using dchv_api.Database;
using dchv_api.Models;

namespace dchv_api.DataRepositories.Implementations;

public class RoleRepository : IRoleRepository
{
    private readonly BaseDbContext _context;
    private readonly ILogger<RecordRepository> _logger;

    public RoleRepository(ILogger<RecordRepository> logger, BaseDbContext dbContext)
    {
        _logger = logger;
        _context = dbContext;
    }

    public Role Add(Role entity)
    {
        _context.Add(entity);
        _context.SaveChanges();
        return _context.Roles.Where(
            (x) => x.ID == entity.ID && x.Deleted_at == null
        ).Single();
    }

    public Task<Role> AddAsync(Role entity)
    {
    throw new NotImplementedException();
    }

    public bool Delete(Role entity)
    {
    throw new NotImplementedException();
    }

    public Task<bool> DeleteAsync(Role entity)
    {
    throw new NotImplementedException();
    }

    public Role? Get(Role entity)
    {
        return _context.Roles.Where((x) =>
            x.ID == entity.ID
            && x.Deleted_at == null
        ).SingleOrDefault();
    }

    public IEnumerable<Role>? GetAll()
    {
        return _context.Roles.Where((x) =>
            x.Deleted_at == null
        );
    }

    public Task<IEnumerable<Role>>? GetAllAsync()
    {
    throw new NotImplementedException();
    }

    public Task<Role>? GetAsync(Role entity)
    {
    throw new NotImplementedException();
    }

    public Role Update(Role entity)
    {
    throw new NotImplementedException();
    }

    public Task<Role> UpdateAsync(Role entity)
    {
    throw new NotImplementedException();
    }
}