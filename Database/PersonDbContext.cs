using System.Security.Permissions;
using dchv_api.Models;
using Microsoft.EntityFrameworkCore;

namespace dchv_api.Database;

public class PersonDbContext : BaseDbContext
{
    private readonly uint _personId;
    public PersonDbContext(DbContextOptions<BaseDbContext> options, uint personId) : base(options)
    {
        _personId = personId;
    }

    protected override void OnModelCreating(ModelBuilder mb)
    {
        mb.Entity<Record>().HasQueryFilter(b => b.PersonID == _personId);
        mb.Entity<RecordData>().HasQueryFilter(b => b.Record != null && b.Record.PersonID == _personId);
        mb.Entity<Person>().HasQueryFilter(b => b.ID == _personId);
        mb.Entity<Contact>().HasQueryFilter(b => b.PersonID == _personId);
        mb.Entity<Login>().HasQueryFilter(b => b.Persons != null && b.Persons.FirstOrDefault((y) => y.ID == _personId) != null);

        base.OnModelCreating(mb);
    }

}
