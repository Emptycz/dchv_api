using Microsoft.AspNetCore.Mvc;
using dchv_api.Models;
using dchv_api.DTOs;
using dchv_api.DataRepositories;
using dchv_api.Functions;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;

namespace dchv_api.Controllers;

[ApiController]
[Route("[controller]")]
public class LoginController : BaseController
{
    private readonly ILoginRepository _repository;
    private readonly ILogger<LoginController> _logger;
    private readonly IMapper _mapper;
    public LoginController(
        ILogger<LoginController> logger,
        ILoginRepository repository,
        IMapper mapper
    )
    {
        _logger = logger;
        _repository = repository;
        _mapper = mapper;
    }

    [Authorize]
    [HttpGet("{id}")]
    public ActionResult<LoginDTO> Get([FromRoute] uint id)
    {
        Login? data = this._repository.Get(new Login{ ID = id });
        if (data is null) return NotFound();
        return Ok(_mapper.Map<Login, LoginDTO>(data));
    }

    [Authorize]
    [HttpGet]
    public ActionResult<IEnumerable<LoginDTO>> Get()
    {
        IEnumerable<Login>? result = _repository.GetAll();
        if (result is null || result.Count() == 0) return NoContent();
        return Ok(_mapper.Map<IEnumerable<Login>, IEnumerable<LoginDTO>>(result));
    }
    
    [HttpPost]
    public ActionResult<LoginDTO> Post([FromBody] Login data)
    {
        // TODO: We need to check if `username` is in email format
        data.Password = CryptographyManager.SHA256(data.Password);
        Login? result = null;
        try {
            result = this._repository.Add(data);
        } catch (Exception ex) {
            _logger.LogDebug("test: {0}", ex.InnerException?.Message);
            if (ex.InnerException is not null && ex.InnerException.Message.Contains("duplicate key")) {
                return Problem("This username is already taken");
            }
            return Problem(ex.Message);
        }
        return Ok(_mapper.Map<Login, LoginDTO>(result));
    }

    [Authorize]
    [HttpDelete("{id}")]
    public ActionResult<bool> Delete([FromRoute] uint id)
    {
        if (id == 0) return BadRequest();
        return this._repository.Delete(new Login{ ID = id});
    }
}
