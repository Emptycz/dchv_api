using AutoMapper;
using dchv_api.DataRepositories;
using dchv_api.DTOs;
using dchv_api.Models;
using dchv_api.RequestModels;
using dchv_api.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace dchv_api.Controllers;

[Authorize]
[ApiController]
[Route("[controller]")]
public class PersonGroupRelationsController : BaseController
{
    private readonly IPersonGroupRelationsRepository _repository;
    private readonly ILogger<PersonGroupRelationsController> _logger;
    private readonly IMapper _mapper;
    private readonly AuthManager _authManager;

    public PersonGroupRelationsController(
        ILogger<PersonGroupRelationsController> logger,
        IPersonGroupRelationsRepository repository,
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
    public async Task<ActionResult<IEnumerable<PersonGroupRelationsDTO>>> Get([FromQuery] PersonGroupRelationsRequest? filter)
    {
        if (filter is not null) {
          filter.PersonID = _authManager.GetPersonID(getLoginId().Value);
        } else {
          filter = new PersonGroupRelationsRequest { PersonID =  _authManager.GetPersonID(getLoginId().Value) };
        }

        IEnumerable<PersonGroupRelations?> result = await _repository.GetAllAsync(filter);
        if (result is null || result.Count() == 0) return NoContent();
        var res = _mapper.Map<IEnumerable<PersonGroupRelationsDTO>>(result).ToList();

        // TODO: This can be optimized, this thing is a hotfix
        for (int i = 0; i < res.Count(); i++)
        {
          res[i].Group!.Members = new List<PersonGroupRelationsDTO>();
        }
        return Ok(res);
    }

    [HttpPatch("{id}")]
    public async Task<ActionResult<PersonGroupRelationsDTO>> Patch([FromRoute] uint id, [FromBody] PersonGroupRelations data)
    {
      // PersonGroupRelations? orig = await _repository.GetAsync(new PersonGroupRelations{ ID = id });
      // if (orig is null) return NotFound();
      // data.ID = orig.ID;
      // data.PersonGroupID = orig.PersonGroupID;
      // data.PersonID = orig.PersonID;
      // data.Created_at = orig.Created_at;
      data.ID = id;
      PersonGroupRelations? res = await _repository.UpdateAsync(new PersonGroupRelations{ID = id, State = PersonGroupRelationState.joined });
      if (res is null) return NotFound();
      return Ok(_mapper.Map<PersonGroupRelations, PersonGroupRelationsDTO>(res));
    }

    [HttpDelete("{id}")]
    public ActionResult<bool> Delete([FromRoute] uint id)
    {
        return this._repository.Delete(new PersonGroupRelations{ ID = id });
    }
}