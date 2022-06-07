namespace HexReader.CoreApplication.Interfaces
{
    public interface IGetDataService
    {
        IEnumerable<BinaryRecord> GetLinesDataFromFile(string filename, long offset);
        long GetFileCountLines(string filename);
    }
}