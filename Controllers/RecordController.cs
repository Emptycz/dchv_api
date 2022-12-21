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
public class RecordController : BaseController
{
    // TODO: Maybe do this as a singleton service?
    private FileHandlerProviderFactory _fileHandlerProviderFactory;
    private readonly IRecordRepository _repository;
    private readonly IRecordDataRepository _dataRepository;
    private readonly ILogger _logger;
    private readonly AuthManager _authManager;
    private readonly IMapper _mapper;

    private readonly string _tempDir = "Temp/UploadedFiles";

    public RecordController(
        ILogger<RecordController> logger,
        IRecordRepository repo,
        IRecordDataRepository dataRepo,
        AuthManager auth,
        IMapper mapper
    )
    {
        _authManager = auth;
        _logger = logger;
        _repository = repo;
        _fileHandlerProviderFactory = new FileHandlerProviderFactory();
        _mapper = mapper;
        _dataRepository = dataRepo;

        prepareTempDirectory();
    }

    [HttpGet]
    public ActionResult<IEnumerable<RecordDTO>> Get([FromQuery] RecordRequest options)
    {
        IEnumerable<Record>? data = this._repository.GetAll(options);
        if (data?.Count() == 0) return NotFound();
        return _mapper.Map<List<RecordDTO>>(data);
    }

    [HttpGet("{id}")]
    public ActionResult<RecordDTO> Get([FromRoute] uint id)
    {
        Record? data = this._repository.Get(new Record{ ID = id });
        if (data is null) return NotFound();
        return Ok(_mapper.Map<RecordDTO>(data));
    }

    [HttpPost]
    public async Task<ActionResult<RecordDTO>> Post([FromForm] RecordDTO record)
    {
        IFormFile? file = Request?.Form?.Files?.FirstOrDefault();
        if (file is null) return BadRequest("file: is a required property");
        if (record is null) return BadRequest("record: server could not auto-map Record");

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

        IEnumerable<RecordData>? recordData;
        try {
            _logger.LogDebug("extension? {0}", extension);
            IFileHandlerProvider fileRepository = _fileHandlerProviderFactory.GetProviderByFileExtension(extension);
            recordData = fileRepository.ReadFromFile(path.ToString());
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

        Record data = new Record{Name = record.Name};
        try {
            data.PersonID = _authManager.GetPersonID(getLoginId().Value);
        } catch (Exception ex)
        {
            _logger.LogError(ex.Message);
            return Unauthorized("User is not logged in");
        }

        RecordDTO response = new RecordDTO();
        try {
            var rec = await this._repository.AddAsync(data);
            for(int key = 0; key < recordData.Count(); key++)
            {
                recordData.ElementAt(key).RecordID = rec.ID;
            }
            var insertRecData = this._dataRepository.AddAsync(recordData);
            response = _mapper.Map<RecordDTO>(rec);
            await insertRecData;
        } catch (Exception ex)
        {
            _logger.LogError(ex.Message);
            return Problem("Server was unable to insert all data into database");
        }
        if (response is null || response.ID == 0) return Problem("Je to špatné");
        return Ok(response);
    }

    // [HttpPatch("{id}")]
    // public async Task<ActionResult<Record>> Patch([FromRoute] uint id, [FromBody] RecordDTO record)
    // {
    //     if (record.Data is null || record.Data.Count == 0) return BadRequest("data: is a required property");
    //     record.ID = id;
    //     for (int i = 0; i < record.Data.Count; i++)
    //     {
    //         record.Data.ElementAt(i).RecordID = id;
    //     }
    //     record.Data = _mapper.Map<ICollection<RecordDataDTO>>(
    //         await this._dataRepository.UpdateAsync(
    //             _mapper.Map<IEnumerable<RecordData>>(record.Data)
    //         )
    //     );
    //     return Ok(record);
    // }

    private bool prepareTempDirectory()
    {
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

