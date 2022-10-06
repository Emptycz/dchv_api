using Microsoft.AspNetCore.Mvc;
using dchv_api.Models;
using dchv_api.FileHandlers;
using dchv_api.Factories;
using dchv_api.DataRepositories;
using dchv_api.Services;
using System.Text.Json;

namespace dchv_api.Controllers;

[ApiController]
[Route("[controller]")]
public class RecordController : BaseController
{
    // TODO: Maybe do this as a singleton service?
    private FileHandlerProviderFactory _fileHandlerProviderFactory;
    private readonly IRecordRepository _repository;
    private readonly ILogger _logger;
    private readonly AuthManager _authManager;

    public RecordController(
        ILogger<RecordController> logger,
        IRecordRepository repo,
        AuthManager auth
    )
    {
        _authManager = auth;
        _logger = logger;
        _repository = repo;
        _fileHandlerProviderFactory = new FileHandlerProviderFactory();
    }

    [HttpPost]
    public async Task<ActionResult<Record>> Post(IFormFile file)
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
            data.AuthorID = _authManager.GetPersonID(getLoginId().Value);
        } catch (Exception ex)
        {
            _logger.LogError(ex.Message);
            return Unauthorized("User is not logged in");
        }

        List<TableColumn>? cols = data.Columns?.Where(
            (x) => x.Name is null || x.Name == String.Empty
        ).ToList();

        if (cols is not null) {
            foreach(TableColumn col in cols)
            {
                _logger.LogDebug("Removing col with name {0}", col.Name);
                data.Columns?.Remove(col);
            }
        }
        // TODO: Save the data to database
        Record response;
        try {
            response = this._repository.Add(data);
        } catch (JsonException ex)
        {
            _logger.LogError(ex.Message);
            return Problem("Server was unable to insert all data into database");
        }
        if (response is null || response.ID == 0) return Problem("Je to špatné");
        return Ok(response);
    }
}
