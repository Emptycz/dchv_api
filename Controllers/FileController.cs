using Microsoft.AspNetCore.Mvc;
using dchv_api.Models;
using dchv_api.DTOs;
using dchv_api.FileHandlers;

namespace dchv_api.Controllers;

[ApiController]
[Route("[controller]")]
public class FileController : ControllerBase
{
    private IFileHandler? _fileRepository;
    private readonly ILogger _logger;
    public FileController(ILogger<FileController> logger)
    {
        _logger = logger;
    }

    [HttpPost]
    public IActionResult Post([FromBody] IFormFile file)
    {
        // TODO: Test this line
        if (file is null) return NoContent();

        ReadOnlySpan<char> fileName = Path.GetFileName(file.FileName);
        ReadOnlySpan<char> path = Path.Combine(Directory.GetCurrentDirectory() + "/Temp/UploadedFiles/", fileName.ToString());

        file.CopyTo(new FileStream(path.ToString(), FileMode.Create));
        // if (nameof(_fileRepository) == Path.GetExtension(fileName)) 
        // {
        //     Path.GetExtension(fileName);
        // }

        // TODO: Make this automatic to all repos inheriting from IFileRepository
        _fileRepository = new CsvHandler();

        _fileRepository.ReadFromFile(path.ToString());
        return Ok();
    }

    // FIXME: We cannot use [FromBody], because apparently there is an error
    // parsing JSON data. Look more into it
    [HttpPatch]
    public ActionResult<TableMeta> Patch(string filename)
    {
        _logger.LogDebug("Loading CSV file {0}", filename);
        _fileRepository = new CsvHandler();
        TableMeta res = _fileRepository.ReadFromFile(Directory.GetCurrentDirectory() + "/Temp/UploadedFiles/" + filename);
        // TODO: Save the {res} into the database.
        return Ok(res);
    }
}