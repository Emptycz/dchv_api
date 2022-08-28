using Microsoft.AspNetCore.Mvc;
using dchv_api.Models;
using dchv_api.DTOs;
using dchv_api.DataRepositories;
using dchv_api.Functions;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;

namespace dchv_api.Controllers;

[Authorize]
[ApiController]
[Route("[controller]")]
public class LoginController : ControllerBase
{
    private readonly ILoginRepository _repository;
    private readonly ILogger<LoginController> _logger;
    
    // TODO: Change this into static abstract function (accepting <T, R>)
    private static readonly MapperConfiguration _mapperConfig = new MapperConfiguration(cfg => cfg.CreateMap<Login, LoginDTO>());
    // TODO: Change this into service
    private Mapper _mapper;
    public LoginController(ILogger<LoginController> logger, ILoginRepository repository)
    {
        _logger = logger;
        _repository = repository;
        _mapper = new Mapper(_mapperConfig);
    }

    [HttpGet("{id}")]
    public ActionResult<LoginDTO> Get([FromRoute] int id)
    {
        Login? data = this._repository.Get(new Login{ ID = id });
        if (data is null) return NotFound();
        return Ok(_mapper.Map<Login, LoginDTO>(data));
    }

    [HttpGet]
    public ActionResult<IEnumerable<LoginDTO>> Get()
    {
        IEnumerable<Login>? result = _repository.GetAll();
        if (result is null || result.Count() == 0) return NoContent();
        //TODO: map IEnumerable<Model> to DTO
        return Ok(result);
    }
    
    [HttpPost]
    public ActionResult<LoginDTO> Post([FromBody] Login data)
    {
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

    [HttpDelete("{id}")]
    public ActionResult<bool> Delete([FromRoute] int id)
    {
        return this._repository.Delete(new Login{ ID = id});
    }
}
