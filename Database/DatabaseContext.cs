using dchv_api.Models;
using Microsoft.EntityFrameworkCore;

namespace dchv_api.Database
{
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

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // NOTE: Probably remove these, because the N:M auto-generated tables are not
            // having this name convention anyway
            modelBuilder.Entity<ContactType>().ToTable("contact_type");
            modelBuilder.Entity<Contact>().ToTable("contact");
            modelBuilder.Entity<Login>().ToTable("login");
            modelBuilder.Entity<Role>().ToTable("role");
            modelBuilder.Entity<Person>().ToTable("person");
            modelBuilder.Entity<House>().ToTable("house");

            // Set Unique constraints
            modelBuilder.Entity<Login>().HasIndex((x) => x.Username).IsUnique();
            modelBuilder.Entity<House>().HasIndex((x) => new { x.Name, x.AuthorID }).IsUnique();
            modelBuilder.Entity<Contact>().HasIndex((x) => new { x.ContactTypeID, x.PersonID, x.Value }).IsUnique();
            modelBuilder.Entity<ContactType>().HasIndex((x) => x.Name).IsUnique();

            base.OnModelCreating(modelBuilder);
        }
    }
}