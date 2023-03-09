using Microsoft.AspNetCore.Mvc;
using dchv_api.Models;
using dchv_api.FileHandlers;
using dchv_api.Factories;
using dchv_api.DataRepositories;
using dchv_api.Services;
using dchv_api.DTOs;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using dchv_api.RequestModels;

namespace dchv_api.Controllers;

[ApiController]
[Authorize]
[Route("[controller]")]
public class RecordSearchController : BaseController
{
    private readonly IRecordRepository _repo;
    private readonly ILogger _logger;
    private readonly AuthManager _authManager;
    private readonly IMapper _mapper;

    public RecordSearchController(
        ILogger<RecordSearchController> logger,
        IRecordRepository repo,
        AuthManager auth,
        IMapper mapper
    )
    {
        _authManager = auth;
        _logger = logger;
        _mapper = mapper;
        _repo = repo;
    }

    [HttpPost]
    public async Task<ActionResult<IEnumerable<RecordDTO>>> Post([FromBody] RecordRequest? filter)
    {
      if (filter is null) return Ok(_mapper.Map<IEnumerable<RecordDTO>>(await _repo.GetAllAsync()));
      return Ok(_mapper.Map<IEnumerable<RecordDTO>>(await _repo.GetAllAsync(filter)));
    }

}
