using AutoMapper;
using dchv_api.DataRepositories;
using dchv_api.DTOs;
using dchv_api.Models;
using dchv_api.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace dchv_api.Controllers;

[Authorize]
[ApiController]
[Route("[controller]")]
public class PersonGroupController : BaseController
{
    private readonly IPersonGroupRepository _repository;
    private readonly ILogger<PersonController> _logger;
    private readonly IMapper _mapper;
    private readonly AuthManager _authManager;

    public PersonGroupController(
        ILogger<PersonController> logger,
        IPersonGroupRepository repository,
        IMapper mapper,
        AuthManager auth
    )
    {
        _logger = logger;
        _repository = repository;
        _mapper = mapper;
        _authManager = auth;
    }

    [HttpGet]
    public ActionResult<IEnumerable<PersonGroupDTO>> Get()
    {
        IEnumerable<PersonGroup>? res = _repository.GetAll();
        if (res is null || res.Count() == 0) return NoContent();
        return Ok(_mapper.Map<IEnumerable<PersonGroup>, IEnumerable<PersonGroupDTO>>(res));
    }

    [HttpGet("{id}")]
    public ActionResult<IEnumerable<PersonGroupDTO>> Get(uint id)
    {
        PersonGroup? res = _repository.Get(new PersonGroup{ID = id});
        if (res is null) return NotFound();
        return Ok(_mapper.Map<PersonGroup, PersonGroupDTO>(res));
    }

    [HttpPost]
    public ActionResult<PersonGroupDTO> Post([FromBody] PersonGroup data)
    {
        data.PersonID = _authManager.GetPersonID(getLoginId().Value);
        PersonGroup? result = null;
        try
        {
            result = this._repository.Add(data);
        } catch (Exception ex)
        {
            _logger.LogError(ex.Message);
            return Problem(ex.Message);
        }
        return Ok(_mapper.Map<PersonGroup, PersonGroupDTO>(result));
    }

    [HttpDelete("{id}")]
    public ActionResult<bool> Delete([FromRoute] uint id)
    {
        if (id == 0) return BadRequest();
        return this._repository.Delete(new PersonGroup{ ID = id});
    }

    // [HttpPatch("{id}")]
    // public ActionResult<PersonGroupDTO> Patch(uint id, [FromBody] PersonGroup data)
    // {
    //     data.PersonID = _authManager.GetPersonID(getLoginId().Value);
    //     PersonGroup? result = null;
    //     try
    //     {
    //         result = this._repository.Update(data);
    //     } catch (Exception ex)
    //     {
    //         _logger.LogError(ex.Message);
    //         return Problem(ex.Message);
    //     }
    //     return Ok(_mapper.Map<PersonGroup, PersonGroupDTO>(result));
    // }

}