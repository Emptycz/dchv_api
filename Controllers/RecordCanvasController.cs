using Microsoft.AspNetCore.Mvc;
using dchv_api.Factories;
using dchv_api.DataRepositories;
using dchv_api.Services;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using dchv_api.RequestModels;

namespace dchv_api.Controllers;

[ApiController]
[Authorize]
[Route("[controller]")]
public class RecordCanvasController : BaseController
{
    // TODO: Maybe do this as a singleton service?
    private FileHandlerProviderFactory _fileHandlerProviderFactory;
    private readonly IRecordCanvasRepository _repository;
    private readonly ILogger _logger;
    private readonly AuthManager _authManager;
    private readonly IMapper _mapper;

    public RecordCanvasController(
        ILogger<RecordCanvasController> logger,
        IRecordCanvasRepository repo,
        AuthManager auth,
        IMapper mapper
    )
    {
        _authManager = auth;
        _logger = logger;
        _repository = repo;
        _fileHandlerProviderFactory = new FileHandlerProviderFactory();
        _mapper = mapper;
    }

    [HttpGet]
    public ActionResult<RecordCanvasDTO> GetAll([FromQuery] RecordCanvasRequest filter)
    {
        var data = this._repository.GetRootDirectoryContent(filter);
        if (data is null) return NoContent();
        return data;
    }

    [HttpGet("{id}")]
    public ActionResult<RecordCanvasDTO> Get([FromRoute] uint id)
    {
        var data = this._repository.GetDirectoryContent(id);
        if (data is null) return NoContent();
        return data;
    }
}

