using System.Text.RegularExpressions;
using dchv_api.Models;

namespace dchv_api.FileHandlers;

public class CSVHandler : IFileHandlerProvider
{
    public string FileExtension { get; } = ".csv";

    public IEnumerable<RecordData> ReadFromFile(string pathToFile, string rowSeparator = ";", string colDelimeter = "\r\n")
    {
        string data;
        try {
            data = File.ReadAllText(pathToFile);
        } catch (Exception ex)
        {
            throw new FileLoadException(ex.Message);
        }

        return ReadFromString(data, rowSeparator, colDelimeter);
    }

    public async Task<IEnumerable<RecordData>> ReadFromFileAsync(string pathToFile, string rowSeparator = ";", string colDelimeter = "\r\n")
    {
        string data;
        try {
            data = await File.ReadAllTextAsync(pathToFile);
        } catch (Exception ex)
        {
            throw new FileLoadException(ex.Message);
        }

        return ReadFromString(data, rowSeparator, colDelimeter);
    }

    public IEnumerable<RecordData> ReadFromString(string data, string colSeparator = ";", string rowSeparator = "\r\n")
    {
        List<RecordData> recordData = new List<RecordData>();
        uint currentRow = 0;
        string[] rows = data.Split(rowSeparator);

        int colsPerRow = Regex.Matches(rows[0], colSeparator).Count() + 1;
        int nextColLimit = colsPerRow;

        // NOTE: Normalize data and replace row separators with col separators
        data = data.Replace(rowSeparator, colSeparator);
        string[] dataByCol = data.Split(colSeparator);

        for (int index = 0; index < dataByCol.Length && index <= nextColLimit; index++)
        {
            if (index == nextColLimit) {
                nextColLimit = nextColLimit + colsPerRow;
                currentRow++;
            }

            dataByCol[index].Trim();

            if (dataByCol[index] == string.Empty || dataByCol[index] is null) continue;
            recordData.Add(new RecordData{
                Row = currentRow,
                Column = (uint) (index - (nextColLimit - (colsPerRow))),
                Value = dataByCol[index]
            });
        }

        return recordData;
    }

    public Task<IEnumerable<RecordData>> ReadFromStringAsync(string data, string rowSeparator = ";", string colDelimeter = "\r\n")
    {
        throw new NotImplementedException();
    }
}