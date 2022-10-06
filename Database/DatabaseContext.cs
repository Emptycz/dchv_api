using dchv_api.Models;
using Microsoft.EntityFrameworkCore;

namespace dchv_api.Database;

public class DatabaseContext : DbContext
{
    public DatabaseContext(DbContextOptions<DatabaseContext> options) : base(options)
    {
    }

    public DbSet<Login> Logins { get; set; }
    // I know this is typo, People is correct according to english
    // but I am trying to be consistent here with table name + "s" pattern
    public DbSet<Person> Persons { get; set; }
    public DbSet<Contact> Contacts { get; set; }
    public DbSet<ContactType> ContactTypes { get; set; }
    public DbSet<Role> Roles { get; set; }
    public DbSet<Record> Records { get; set; }
    public DbSet<TableColumn> TableColumns { get; set; }
    public DbSet<TableData> TableDatas { get; set; }
    public DbSet<TableGroup> TableGroups { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        // NOTE: Probably remove these, because the N:M auto-generated tables are not
        // having this name convention anyway
        modelBuilder.Entity<ContactType>().ToTable("contact_type");
        modelBuilder.Entity<Contact>().ToTable("contact");
        modelBuilder.Entity<Login>().ToTable("login");
        modelBuilder.Entity<Role>().ToTable("role");
        modelBuilder.Entity<Person>().ToTable("person");
        modelBuilder.Entity<PersonGroup>().ToTable("person_group");

        // Set Unique constraints
        modelBuilder.Entity<Login>().HasIndex((x) => x.Username).IsUnique();
        modelBuilder.Entity<PersonGroup>().HasIndex((x) => new { x.Name, x.AuthorID }).IsUnique();
        modelBuilder.Entity<Contact>().HasIndex((x) => new { x.ContactTypeID, x.PersonID, x.Value }).IsUnique();
        modelBuilder.Entity<ContactType>().HasIndex((x) => x.Name).IsUnique();
        modelBuilder.Entity<TableColumn>().HasIndex((x) => x.Name).IsUnique();

        base.OnModelCreating(modelBuilder);
    }
}
