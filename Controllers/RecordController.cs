using Microsoft.AspNetCore.Mvc;
using dchv_api.Models;
using dchv_api.FileHandlers;
using dchv_api.Factories;
using dchv_api.DataRepositories;
using dchv_api.Services;
using dchv_api.DTOs;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;

namespace dchv_api.Controllers;

[ApiController]
[Authorize]
[Route("[controller]")]
public class RecordController : BaseController
{
    // TODO: Maybe do this as a singleton service?
    private FileHandlerProviderFactory _fileHandlerProviderFactory;
    private readonly IRecordRepository _repository;
    private readonly ILogger _logger;
    private readonly AuthManager _authManager;
    private readonly IMapper _mapper;

    private readonly string _tempDir = "/Temp/UploadedFiles";

    public RecordController(
        ILogger<RecordController> logger,
        IRecordRepository repo,
        AuthManager auth,
        IMapper mapper
    )
    {
        _authManager = auth;
        _logger = logger;
        _repository = repo;
        _fileHandlerProviderFactory = new FileHandlerProviderFactory();
        _mapper = mapper;

        prepareTempDirectory();
    }

    [HttpGet]
    public ActionResult<IEnumerable<RecordDTO>> Get()
    {
        return _mapper.Map<List<RecordDTO>>(_repository.GetAll());
    }

    [HttpGet("{id}")]
    public ActionResult<RecordDTO> Get([FromRoute] uint id)
    {
        Record? data = this._repository.Get(new Record{ ID = id });
        if (data is null) return NotFound();
        return Ok(_mapper.Map<RecordDTO>(data));
    }

    [HttpPost]
    public async Task<ActionResult<RecordDTO>> Post(IFormFile file)
    {
        if (file is null) return BadRequest("file: is a required property");

        string fileName = Guid.NewGuid().ToString();
        string path = Path.Combine(Directory.GetCurrentDirectory() + "/Temp/UploadedFiles/", fileName.ToString());
        string extension = Path.GetExtension(file.FileName);

        try {
            using (FileStream fs = new FileStream(path.ToString(), FileMode.CreateNew)) {
                await file.CopyToAsync(fs);
            }
        } catch (FileNotFoundException) {
            return BadRequest("No file was uploaded");
        } catch (Exception ex) {
            return Problem(ex.Message);
        }

        Record data;
        try {
            _logger.LogDebug("extension? {0}", extension);
            IFileHandlerProvider fileRepository = _fileHandlerProviderFactory.GetProviderByFileExtension(extension);
             data = fileRepository.ReadFromFile(path.ToString());
        } catch (NotImplementedException ex) {
            _logger.LogError(ex.Message);
            return Problem(ex.Message);
        } catch (Exception ex) {
            _logger.LogError(ex.Message);
            return Problem("Server was unable to process the file");
        }

        // Delete the physical file from API
        try {
            System.IO.File.Delete(path);
        } catch (Exception ex) {
            _logger.LogError(ex.Message);
            return Problem("Server was unable to remove the file.");
        }

        data.Name = file.FileName;
        try {
            data.PersonID = _authManager.GetPersonID(getLoginId().Value);
        } catch (Exception ex)
        {
            _logger.LogError(ex.Message);
            return Unauthorized("User is not logged in");
        }

        RecordDTO response = new RecordDTO();
        try {
            response = _mapper.Map<RecordDTO>(this._repository.Add(data));
        } catch (Exception ex)
        {
            _logger.LogError(ex.Message);
            return Problem("Server was unable to insert all data into database");
        }
        if (response is null || response.ID == 0) return Problem("Je to špatné");
        return Ok(response);
    }

    private bool prepareTempDirectory()
    {
        // FIXME: Probably does not work, this also should be separate fn
        //        that should be triggered upon app start, so move it into
        //        program.cs
        if (Directory.Exists(Directory.GetCurrentDirectory() + this._tempDir)) {
            return true;
        }

        try
        {
            Directory.CreateDirectory(this._tempDir);
            return true;
        } catch (Exception ex)
        {
            _logger.LogCritical(ex.Message);
            return false;
        }
    }
}
