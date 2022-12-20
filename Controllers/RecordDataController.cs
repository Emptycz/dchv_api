using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using dchv_api.Services;
using AutoMapper;
using dchv_api.Models;
using dchv_api.DataRepositories;
using dchv_api.DTOs;

namespace dchv_api.Controllers;

[ApiController]
[Authorize]
[Route("[controller]")]
public class RecordDataController : BaseController
{
  private readonly IRecordDataRepository _repository;
  private readonly ILogger _logger;
  private readonly AuthManager _authManager;
  private readonly IMapper _mapper;

  public RecordDataController(
    ILogger<RecordDataController> logger,
    IRecordDataRepository repo,
    AuthManager auth,
    IMapper mapper
  )
  {
    _authManager = auth;
    _logger = logger;
    _repository = repo;
    _mapper = mapper;
  }

  [HttpGet]
  public ActionResult<IEnumerable<RecordDataDTO>> Get([FromQuery] RecordDataDTO? filter)
  {
    IEnumerable<RecordData>? data;
    if (filter is null)
    {
      data = this._repository.GetAll();
    } else {
      data = this._repository.GetAll(_mapper.Map<RecordData>(filter));
    }
    return Ok(_mapper.Map<IEnumerable<RecordDataDTO>>(data));
  }

  [HttpPatch]
  public ActionResult<IEnumerable<RecordDataDTO>> Patch([FromBody] IEnumerable<RecordData> data)
  {
    try
    {
      IEnumerable<RecordData>? result = this._repository.Update(data);
      if (result is null || result.FirstOrDefault() is null) return NotFound();
      return Ok(_mapper.Map<IEnumerable<RecordDataDTO>>(result));
    }
      catch (Exception ex)
    {
      return Problem(ex.Message);
    }
  }
}