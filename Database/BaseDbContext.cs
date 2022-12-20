using System;
using dchv_api.Models;
using Microsoft.EntityFrameworkCore;

namespace dchv_api.Database;

public class BaseDbContext : DbContext
{
    public BaseDbContext(DbContextOptions<BaseDbContext> options) : base(options)
    {
    }

    public DbSet<Login> Login { get; set; }
    public DbSet<Person> Person { get; set; }
    public DbSet<Contact> Contact { get; set; }
    public DbSet<ContactType> ContactType { get; set; }
    public DbSet<Role> Role { get; set; }
    public DbSet<Record> Record { get; set; }
    public DbSet<RecordData> RecordData { get; set; }
    public DbSet<RecordGroup> RecordGroup { get; set; }
    public DbSet<PersonGroup> PersonGroup { get; set; }

    // FIXME: Run this only when debuging (Development run)
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        => optionsBuilder
            .LogTo(Console.WriteLine, LogLevel.Debug)
            .EnableDetailedErrors();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.UseCollation("Czech_CI_AS");

        // Set Unique constraints
        modelBuilder.Entity<Login>().HasIndex((x) => x.Username).IsUnique();
        modelBuilder.Entity<Contact>().HasIndex((x) => new { x.ContactTypeID, x.PersonID, x.Value }).IsUnique();
        modelBuilder.Entity<PersonGroup>().HasIndex(nameof(dchv_api.Models.PersonGroup.Name), nameof(dchv_api.Models.PersonGroup.PersonID)).IsUnique();

        modelBuilder.Entity<ContactType>().HasIndex((x) => x.Name).IsUnique();

        modelBuilder.Entity<PersonGroupRelations>()
            .HasKey((x) => x.ID);
        modelBuilder.Entity<PersonGroupRelations>()
            .HasIndex((x) => new { x.PersonID, x.PersonGroupID, x.Deleted_at }).IsUnique();

        modelBuilder.Entity<Record>()
            .HasOne(x => x.RecordGroup)
            .WithMany(x => x.Records)
            .OnDelete(DeleteBehavior.NoAction);

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

        modelBuilder.Entity<RecordGroup>()
            .HasMany(x => x.ChildGroups)
            .WithOne()
            .IsRequired(false)
            .OnDelete(DeleteBehavior.NoAction);

        // FIXME: Need to create trigger for this
        modelBuilder.Entity<RecordData>()
            .Property(x => x.Modified_at).ValueGeneratedOnUpdate();

        // modelBuilder.Entity<Record>()
        //     .HasMany(x => x.Tags)
        //     .WithMany(x => x.Record);

        base.OnModelCreating(modelBuilder);
    }

}
