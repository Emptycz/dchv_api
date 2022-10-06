using dchv_api.DataRepositories;
using dchv_api.Models;

namespace dchv_api.Services;

public class AuthManager 
{
    private readonly IPersonRepository _repository;
    private readonly ILogger<AuthManager> _logger;
 
    public AuthManager(
        IPersonRepository personRepo,
        ILogger<AuthManager> logger
    )
    {
        _logger = logger;
        _repository = personRepo;
    }

    public uint GetPersonID(uint loginID)
    {
        Person? person = _repository.GetByLoginId(loginID).SingleOrDefault();
        if (person is null)
        {
            _logger.LogError("Login {0} does not have any active Persons", loginID);
            throw new Exception("Person for this user does not exist");
        }
        return person.ID;
    }
}