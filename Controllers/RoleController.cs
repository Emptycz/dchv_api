using System.Globalization;
using Microsoft.AspNetCore.Mvc;
using dchv_api.Models;
using dchv_api.FileHandlers;
using dchv_api.Factories;
using dchv_api.DataRepositories;
using dchv_api.Services;
using System.Text.Json;
using Microsoft.AspNetCore.Authorization;
using dchv_api.DTOs;
using AutoMapper;

namespace dchv_api.Controllers;

[ApiController]
[Route("[controller]")]
[Authorize]
public class RoleController : BaseController
{
    private readonly IRoleRepository _repository;
    private readonly IMapper _mapper;
    private readonly AuthManager _authManager;

    public RoleController(
        IRoleRepository repo,
        IMapper mapper,
        AuthManager auth
    )
    {
        _authManager = auth;
        _repository = repo;
        _mapper = mapper;
    }

    [HttpGet]
    public ActionResult<IEnumerable<RoleDTO>> Get()
    {
        IEnumerable<Role>? result = _repository.GetAll();
        if (result is null) return NoContent();
        return Ok(_mapper.Map<IEnumerable<Role>, IEnumerable<RoleDTO>>(result));
    }

    [HttpGet("{id}")]
    public ActionResult<RoleDTO> Get(int id)
    {
        Role? result = (_repository.Get(new Role{ID = id}));
        if (result is null) return NotFound();
        return Ok(_mapper.Map<Role, RoleDTO>(result));
    }

    [HttpPost]
    public ActionResult<RoleDTO> Post([FromBody] RoleDTO role)
    {
        Role result;

        role.PersonID = _authManager.GetPersonID(getLoginId().Value);
        try {
            result = _repository.Add(_mapper.Map<RoleDTO, Role>(role));
        } catch (Exception ex)
        {
            return Problem(ex.Message);
        }
        return Ok(_mapper.Map<Role, RoleDTO>(result));
    }

}