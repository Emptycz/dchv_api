using dchv_api.Models;
using dchv_api.Database;

namespace dchv_api.DataRepositories.Implementations
{
    public class PersonRepository : IPersonRepository
    {
        private readonly DatabaseContext _context;
        private readonly ILogger<PersonRepository> _logger;
        public PersonRepository(ILogger<PersonRepository> logger, DatabaseContext dbContext)
        {
            _logger = logger;
            _context = dbContext;
        }
        public Person Add(Person entity)
        {
            _context.Add(entity);
            _context.SaveChanges();
            return this.Get(entity);
        }

        public Task<Person> AddAsync(Person entity)
        {
            throw new NotImplementedException();
        }

        public Person? Get(Person entity)
        {
            return _context.Persons.Where((x) => x.ID == entity.ID && x.Deleted_at == null).SingleOrDefault();
        }

        public IEnumerable<Person>? GetAll()
        {
            return _context.Persons.Where((x) => x.Deleted_at == null);
        }

        public Task<IEnumerable<Person>> GetAllAsync()
        {
            throw new NotImplementedException();
        }

        public Task<Person> GetAsync(Person entity)
        {
            throw new NotImplementedException();
        }

        public bool Delete(Person entity)
        {
            entity = this.Get(entity);
            if (entity is null) return false;
            entity.Deleted_at = DateTime.UtcNow;
            _context.Update<Person>(entity);
            return _context.SaveChanges() > 0 ? true : false;        }

        public Task<bool> DeleteAsync(Person entity)
        {
            throw new NotImplementedException();
        }

        public Person Update(Person entity)
        {
            throw new NotImplementedException();
        }

        public Task<Person> UpdateAsync(Person entity)
        {
            throw new NotImplementedException();
        }
    }
}