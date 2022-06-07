namespace HexReader.CoreApplication.Interfaces
{
    public interface IGetDataService
    {
        IEnumerable<BinaryRecord> GetLinesDataFromFile(string filename, int offset);
    }
}