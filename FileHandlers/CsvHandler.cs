using dchv_api.Models;

namespace dchv_api.FileHandlers;

    // NOTE: Just a prototyp of CSV handler. Might and will probably change in the future.   
public class CsvHandler : IFileHandlerProvider 
{
    public string FileExtension { get; } = ".csv";

    public Record ReadFromFile(string pathToFile, string rowSeparator = ";", string colDelimeter = "\r\n")
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

    public Task<Record> ReadFromFileAsync(string pathToFile, string rowSeparator = ";", string colDelimeter = "\r\n")
    {
        throw new NotImplementedException();
    }

    public Record ReadFromString(string data, string rowSeparator = ";", string colDelimeter = "\r\n")
    {
        if (data.Length == 0) throw new Exception("No data provided");
        List<TableColumn> cols = new List<TableColumn>();

        // Number of columns in the whole CSV file
        int maxCols = 0; 
        // Helper to calculate the current iteration of column
        // (while going through data-values)
        int elementsInRow = 0;

        // NOTE: Last column has no {;} separator, we need
        // to add this separator so we can easily extract
        // values in the last column
        data = data.Replace(colDelimeter, ";\r\n");

        ReadOnlySpan<string> columns = data.Split(rowSeparator);

        for (int i = 0; i < columns.Length; i++)
        {
            if (maxCols != 0)
            {             
                // NOTE: we have to check for multiple rows...
                // because the {i} will be always increasing so we can
                // get outside of the list lenght (in case we go to third
                // row, we can get to {19-9} which is 10, but we have
                // only 9 columns, yet we are trying to update non-existing 
                // column number 10)
                TableData rowData = new TableData();

                if (maxCols == (i - elementsInRow)) 
                {
                    // NOTE: This is the first element in [] which
                    // has the {\r\n} at start, we need to remove that
                    rowData.Value = columns[i].Split("\n")[1];

                    // Set calc limit that tells when is new row
                    elementsInRow = elementsInRow + maxCols;
                } else {
                    rowData.Value = columns[i];
                }

                // TODO:
                // We need to check and solve lists (if they are alowed in CSV files)
                rowData.ListKey = 1;
                rowData.ListName = null;

                // NOTE:
                // Save the List of TableData class in TableColumn.Values
                // We are trying to save the data in columns (meaning each column will have
                // list of values + row numbers)
                cols[i - elementsInRow].Values?.Add(rowData);
            }
            else
            {
                TableColumn com = new TableColumn();

                ReadOnlySpan<string> colName = columns[i].Split("\r\n");
                if (columns[i].Contains("\n"))
                {
                    // NOTE: save the exact numbers of cols and 
                    // use it, now you know how many cols are there
                    // so you dont have to check for this symbol ever again
                    maxCols = i;
                    elementsInRow = i;

                    // NOTE: The last column name has also the first element with it
                    // (after the row separator symbol). We need to extract it and 
                    // map it to the array of vals
                    cols[0].Values?.Add(new TableData{ Value = colName[1] });
                }
                // // If column has no name (value), skip it
                // if (colName[0] is null || colName[0] == String.Empty)
                // {
                //     if (i < columns.Length) continue;
                //     break;
                // }

                com.Name = colName[0];
                com.Position = i + 1;
                cols.Add(com);
            }
        }
        return new Record{ Columns = cols };
    }

    public Task<Record> ReadFromStringAsync(string data, string rowSeparator = ";", string colDelimeter = "\r\n")
    {
    throw new NotImplementedException();
    }
}
