using Microsoft.AspNetCore.Mvc;
using dchv_api.Models;
using dchv_api.DataRepositories;
using dchv_api.Services;
using dchv_api.Functions;
using dchv_api.DTOs;
using System.IdentityModel.Tokens.Jwt;

namespace dchv_api.Controllers;

[ApiController]
[Route("[controller]")]
public class AuthController : ControllerBase
{
    private readonly ILogger<AuthController> _logger;
    private readonly ILoginRepository _repository;
    private readonly JwtManager _jwtManager;
    public AuthController(
        ILogger<AuthController> logger,
        ILoginRepository repository,
        JwtManager jwtManager
    )
    {
        _logger = logger;
        _repository = repository;
        _jwtManager = jwtManager;
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
        // TODO: Login user internally & hold the JWT token
        string jwtToken = new JwtSecurityTokenHandler().WriteToken(token);
        return Ok(new AuthDTO{
            Token = jwtToken,
            Username = data.Username,
        });
    }

}

