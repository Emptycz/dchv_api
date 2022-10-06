using dchv_api.Models;

namespace dchv_api.FileHandlers;

public interface IFileHandlerProvider
{
    /// <summary>
    /// Process content of given table file, such as CSV file.
    /// </summary>
    /// <param name="pathToFile"> Path to the file </param>
    /// <returns> Initializies new instance of TableMeta class. </returns>
    Record ReadFromFile(string pathToFile, string rowSeparator = ";", string colDelimeter = "\r\n");

    /// <summary>
    /// Process content of given string.
    /// </summary>
    /// <param name="data"> String holding all the data that will be processed. </param>
    /// <param name="rowSeparator"> Row separator </param>
    /// <param name="colDelimeter"> Column delimeter </param>
    /// <returns> Initializies new instance of TableMeta class. </returns>
    Record ReadFromString(string data, string rowSeparator = ";", string colDelimeter = "\r\n");

    /// <summary>
    /// Process content of given table file, such as CSV file.
    /// </summary>
    /// <param name="pathToFile"> Path to the file </param>
    /// <returns> Initializies new instance of TableMeta class. </returns>
    Task<Record> ReadFromFileAsync(string pathToFile, string rowSeparator = ";", string colDelimeter = "\r\n");

    /// <summary>
    /// Process content of given string.
    /// </summary>
    /// <param name="data"> String holding all the data that will be processed. </param>
    /// <param name="rowSeparator"> Row separator </param>
    /// <param name="colDelimeter"> Column delimeter </param>
    /// <returns> Initializies new instance of TableMeta class. </returns>
    Task<Record> ReadFromStringAsync(string data, string rowSeparator = ";", string colDelimeter = "\r\n");

    public string FileExtension { get; }
}
