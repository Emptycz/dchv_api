using Microsoft.AspNetCore.Mvc;
using dchv_api.Models;
using dchv_api.DTOs;
using dchv_api.DataRepositories;
using Microsoft.AspNetCore.Authorization;

namespace dchv_api.Controllers;

[Authorize]
[ApiController]
[Route("[controller]")]
public class PersonController : BaseController
{
    private readonly IPersonRepository _repository;
    private readonly ILogger<PersonController> _logger;
    public PersonController(ILogger<PersonController> logger, IPersonRepository repository)
    {
        _logger = logger;
        _repository = repository;
    }

    [HttpGet("{id}")]
    public ActionResult<PersonDTO> Get([FromRoute] uint id)
    {
        Person? data = this._repository.Get(new Person{ ID = id });
        if (data is null) return NotFound();
        //TODO: map DTO to Model
        return Ok(data);
    }

    [HttpGet]
    public ActionResult<IEnumerable<PersonDTO>> Get()
    {
        IEnumerable<Person>? result = _repository.GetAll();
        if (result is null || result.Count() == 0) return NoContent();
        //TODO: map DTO to Model
        return Ok(result);
    }

    [HttpPost]
    public ActionResult<PersonDTO> Post([FromBody] PersonDTO entity)
    {
        Person? result = null;
        try {
            result = this._repository.Add(new Person{
                Firstname = entity.Firstname,
                Lastname = entity.Lastname,
                LoginID = getLoginId().Value
            });
        } catch (Exception ex) {
            _logger.LogError(ex.Message);
            return Problem(ex.Message);
        }
        //TODO: map DTO to Model
        return Ok(result);
    }

    [HttpDelete("{id}")]
    public ActionResult<bool> Delete([FromRoute] uint id)
    {
        return this._repository.Delete(new Person{ ID = id});
    }
}