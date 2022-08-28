using Microsoft.AspNetCore.Mvc;
using dchv_api.Models;
using dchv_api.DTOs;
using dchv_api.DataRepositories;
using Microsoft.AspNetCore.Authorization;

namespace dchv_api.Controllers;

[Authorize]
[ApiController]
[Route("[controller]")]
public class PersonController : ControllerBase
{
    private readonly IPersonRepository _repository;
    private readonly ILogger<PersonController> _logger;

    public PersonController(ILogger<PersonController> logger, IPersonRepository repository)
    {
        _logger = logger;
        _repository = repository;
    }

    [HttpGet("{id}")]
    public ActionResult<PersonDTO> Get([FromRoute] int id)
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
    public ActionResult<PersonDTO> Post([FromBody] Person entity)
    {
        Person? result = null;
        try {
            result = this._repository.Add(entity);
        } catch (Exception ex) {
            return Problem(ex.Message);
        }
        //TODO: map DTO to Model
        return Ok(result);
    }

    [HttpDelete("{id}")]
    public ActionResult<bool> Delete([FromRoute] int id)
    {
        return this._repository.Delete(new Person{ ID = id});
    }
}