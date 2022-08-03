using Microsoft.AspNetCore.Mvc;
using dchv_api.Models;
using dchv_api.DTOs;
using dchv_api.DataRepositories;

namespace dchv_api.Controllers;

[ApiController]
[Route("[controller]")]
public class PersonController : ControllerBase
{
    private readonly IPersonRepository _repository;

    public PersonController(IPersonRepository repository)
    {
        _repository = repository;
    }

    [HttpGet]
    public ActionResult<PersonDTO> Get()
    {
        throw new NotImplementedException();
    }

    [HttpGet("{id}")]
    public ActionResult<IEnumerable<PersonDTO>> Get([FromQuery] int id)
    {
        throw new NotImplementedException();
    }

    [HttpPost]
    public ActionResult<PersonDTO> Post([FromBody] PersonDTO entity)
    {
        throw new NotImplementedException();
    }

    [HttpDelete("{id}")]
    public ActionResult<bool> Delete([FromQuery] int id)
    {
        return Ok(this._repository.Delete(new Person{ ID = id }));
    }
}