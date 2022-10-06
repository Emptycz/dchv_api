using Microsoft.AspNetCore.Mvc;
using dchv_api.Models;
using dchv_api.DataRepositories;
using dchv_api.Services;
using dchv_api.Functions;
using dchv_api.DTOs;
using System.IdentityModel.Tokens.Jwt;
using System.Text.Json;
using AutoMapper;

namespace dchv_api.Controllers;

[ApiController]
[Route("[controller]")]
public class AuthController : ControllerBase
{
    private readonly ILogger<AuthController> _logger;
    private readonly ILoginRepository _repository;
    private readonly JwtManager _jwtManager;
    private readonly IMapper _mapper;
    public AuthController(
        ILogger<AuthController> logger,
        ILoginRepository repository,
        JwtManager jwtManager,
        IMapper mapper
    )
    {
        _logger = logger;
        _repository = repository;
        _jwtManager = jwtManager;
        _mapper = mapper;
    }

    [HttpPost]
    public ActionResult<AuthDTO> Post([FromBody] Login data)
    {
        data.Password = CryptographyManager.SHA256(data.Password);
        Login? login = _repository.LoginUser(data);
        
        if (login is null) return NotFound("Invalid credentials");
        _logger.LogDebug("User {0} was found and verified", login.Username);
        JwtSecurityToken token = _jwtManager.GenerateToken(login);
        
        if (token is null) return Problem("Token could not be generated");

        string jwtToken = new JwtSecurityTokenHandler().WriteToken(token);

        var persons = _mapper.Map<List<PersonDTO>>(login.Persons);

        return Ok(new AuthDTO{
            Token = jwtToken,
            Username = login.Username,
            ID = login.ID,
            Verified_at = login.Verified_at,
            Created_at = login.Created_at,
            Modified_at = login.Modified_at,
            Deleted_at = login.Deleted_at,
            Persons = persons,
        });
    }

}

