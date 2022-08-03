using Microsoft.AspNetCore.Mvc;
using dchv_api.Models;
using dchv_api.DTOs;
using dchv_api.DataRepositories;

namespace dchv_api.Controllers;

[ApiController]
[Route("[controller]")]
public class LoginController : ControllerBase
{
    private readonly ILoginRepository _repository;
    public LoginController(ILoginRepository repository)
    {
        _repository = repository;
    }

    [HttpGet("{id}")]
    public LoginDTO Get([FromQuery] int id)
    {
        // return this._repository.Get(new Login{ ID = id });
        throw new NotImplementedException();
    }

    [HttpGet]
    public ActionResult<IEnumerable<LoginDTO>> Get()
    {
        // map DTO to Model
        // return this._repository.GetAll();
        throw new NotImplementedException();
    }
    
    [HttpPost]
    public ActionResult<LoginDTO> Post([FromBody] LoginDTO data)
    {
        // map DTO to Model
        // return this._repository.Add(data);
        throw new NotImplementedException();
    }

    [HttpDelete("{id}")]
    public ActionResult<bool> Delete([FromQuery] int id)
    {
        // map DTO to Model
        // return this._repository.Delete(data);
        throw new NotImplementedException();
    }
}
