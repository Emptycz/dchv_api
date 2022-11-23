using System.Diagnostics;
using System.Security.Permissions;
using dchv_api.Models;
using Microsoft.EntityFrameworkCore;

namespace dchv_api.Database;

public class BaseDbContext : DbContext
{
    public BaseDbContext(DbContextOptions<BaseDbContext> options) : base(options)
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
    public DbSet<PersonGroup> PersonGroups { get; set; }

    // FIXME: Run this only when debuging (Development run)
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        => optionsBuilder
            .LogTo(Console.WriteLine, LogLevel.Information)
            .EnableDetailedErrors();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        // Set Unique constraints
        modelBuilder.Entity<Login>().HasIndex((x) => x.Username).IsUnique();
        modelBuilder.Entity<PersonGroup>().HasIndex((x) => new { x.Name, x.PersonID }).IsUnique();
        modelBuilder.Entity<Contact>().HasIndex((x) => new { x.ContactTypeID, x.PersonID, x.Value }).IsUnique();
        modelBuilder.Entity<ContactType>().HasIndex((x) => x.Name).IsUnique();
        modelBuilder.Entity<TableColumn>().HasIndex((x) => x.Name).IsUnique();

        modelBuilder.Entity<PersonGroupRelations>()
            .HasKey((x) => new { x.PersonID, x.PersonGroupID });
        modelBuilder.Entity<PersonGroupRelations>()
            .ToTable("person_group_relations");

        modelBuilder.Entity<Record>()
            .Property((x) => x.Name).HasColumnName("Name").IsUnicode(true);

        modelBuilder.Entity<PersonGroupRelations>()
            .HasOne(x => x.Person)
            .WithMany(x => x.PersonGroups)
            .HasForeignKey(x => x.PersonID)
            .OnDelete(DeleteBehavior.NoAction);

        modelBuilder.Entity<PersonGroupRelations>()
            .HasOne(x => x.Group)
            .WithMany(x => x.Members)
            .HasForeignKey(x => x.PersonGroupID)
            .OnDelete(DeleteBehavior.NoAction);

        base.OnModelCreating(modelBuilder);
    }

}
