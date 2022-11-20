using System;
using System.Linq;
using dchv_api.Functions;
using dchv_api.Models;

namespace dchv_api.Database.Seed;

public static class DbSampleSeed
{
    public static void Initialize(BaseDbContext context)
    {
        context.Database.EnsureDeleted();
        context.Database.EnsureCreated();
        if (context.Logins.Any())
        {
            return;   // DB has been seeded
        }

        var roles = new Role[2] {
            new Role{Slug = Enums.RolesEnum.Admin, Name = "Admin"},
            new Role{Slug = Enums.RolesEnum.User, Name = "User"},
        };

        foreach (Role e in roles)
        {
            context.Roles.Add(e);
        }

        string encryptedPassword = CryptographyManager.SHA256("123");
        var logins = new Login[3] {
            new Login{Username = "admin@test.com", Password = encryptedPassword,
                        Persons = new Person[1]{new Person{
                            Firstname = "Pan", Lastname = "Admin", Roles = new Role[1]{roles[0]}}}},
            new Login{Username = "user@test.com", Password = encryptedPassword,
                        Persons = new Person[1]{new Person{
                            Firstname = "Jan", Lastname = "Kočvara", Roles = new Role[1]{roles[1]}}}},
            new Login{Username = "test@test.com", Password = encryptedPassword,
                        Persons = new Person[1]{new Person{
                            Firstname = "Test", Lastname = "Testikus", Roles = new Role[1]{roles[1]}}}},
        };

        foreach (Login e in logins)
        {
            context.Logins.Add(e);
        }

        var records = new Record[5] {
            new Record{Person = logins[0].Persons?.FirstOrDefault(), Name = "Admin record", Data = new List<RecordData>{
                new RecordData{Row = 0, Column = 0, Value = "Key"},
                new RecordData{Row = 0, Column = 1, Value = "Val"},
                new RecordData{Row = 1, Column = 0, Value = "A"},
                new RecordData{Row = 1, Column = 1, Value = "B"},
            }},
            new Record{Person = logins[0].Persons?.FirstOrDefault(), Name = "Admin record 2", Data = new List<RecordData>{
                new RecordData{Row = 0, Column = 0, Value = "Key"},
                new RecordData{Row = 0, Column = 1, Value = "Val"},
                new RecordData{Row = 1, Column = 0, Value = "A"},
                new RecordData{Row = 1, Column = 1, Value = "B"},
            }},
            new Record{Person = logins[1].Persons?.FirstOrDefault(), Name = "Kočvara record", Data = new List<RecordData>{
                new RecordData{Row = 0, Column = 0, Value = "Key"},
                new RecordData{Row = 0, Column = 1, Value = "Val"},
                new RecordData{Row = 1, Column = 0, Value = "A"},
                new RecordData{Row = 1, Column = 1, Value = "B"},
            }},
            new Record{Person = logins[1].Persons?.FirstOrDefault(), Name = "Jan K record", Data = new List<RecordData>{
                new RecordData{Row = 0, Column = 0, Value = "Key"},
                new RecordData{Row = 0, Column = 1, Value = "Val"},
                new RecordData{Row = 1, Column = 0, Value = "A"},
                new RecordData{Row = 1, Column = 1, Value = "B"},
            }},
            new Record{Person = logins[2].Persons?.FirstOrDefault(), Name = "User record", Data = new List<RecordData>{
                new RecordData{Row = 0, Column = 0, Value = "Key"},
                new RecordData{Row = 0, Column = 1, Value = "Val"},
                new RecordData{Row = 1, Column = 0, Value = "A"},
                new RecordData{Row = 1, Column = 1, Value = "B"},
            }},
        };

        foreach (Record e in records)
        {
            context.Records.Add(e);
        }

        context.SaveChanges();
    }
}
