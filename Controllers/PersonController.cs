using Microsoft.AspNetCore.Mvc;
using dchv_api.Models;
using dchv_api.DTOs;
using dchv_api.DataRepositories;
using Microsoft.AspNetCore.Authorization;
using dchv_api.RequestModels;
using AutoMapper;

namespace dchv_api.Controllers;

[Authorize]
[ApiController]
[Route("[controller]")]
public class PersonController : BaseController
{
    private readonly IPersonRepository _repository;
    private readonly ILogger<PersonController> _logger;
    private readonly IMapper _mapper;

    public PersonController(
        ILogger<PersonController> logger,
        IPersonRepository repository,
        IMapper mapper
    )
    {
        _logger = logger;
        _repository = repository;
        _mapper = mapper;
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<PersonDTO>> Get([FromRoute] uint id)
    {
        Person? data = await this._repository.GetAsync(new Person{ ID = id });
        if (data is null) return NotFound();
        return Ok(_mapper.Map<PersonDTO>(data));
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<PersonDTO>>> Get([FromQuery] PersonRequest filter)
    {
        IEnumerable<Person?> result = await _repository.GetAllAsync(filter);
        if (result is null || result.Count() == 0) return NoContent();
        return Ok(_mapper.Map<IEnumerable<PersonDTO>>(result));
    }

    [HttpPost]
    public async Task<ActionResult<PersonDTO>> Post([FromBody] PersonDTO entity)
    {
        Person? result = null;
        try {
            result = await this._repository.AddAsync(new Person{
                Firstname = entity.Firstname,
                Lastname = entity.Lastname,
                LoginID = getLoginId().Value
            });
        } catch (Exception ex) {
            _logger.LogError(ex.Message);
            return Problem(ex.Message);
        }
        return Ok(_mapper.Map<PersonDTO>(result));
    }

    [HttpDelete("{id}")]
    public ActionResult<bool> Delete([FromRoute] uint id)
    {
        return this._repository.Delete(new Person{ ID = id});
    }
}