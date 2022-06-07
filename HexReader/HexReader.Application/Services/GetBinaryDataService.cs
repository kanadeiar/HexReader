namespace HexReader.CoreApplication.Services;

public class GetBinaryDataService : IGetDataService
{
    private readonly IFileReaderHelper _readerService;
    private readonly Encoding _encoding;
    public GetBinaryDataService(IFileReaderHelper readerService)
    {
        _readerService = readerService;
        _encoding = Encoding.GetEncoding(1251);
    }

    public IEnumerable<BinaryRecord> GetLinesDataFromFile(string filename, long offset)
    {
        var lines = _readerService.ReadBinaryLinesWithOffset(filename, offset, 30);
        var sb16 = new StringBuilder(16);
        var list = lines.Where(x => x is { }).Select((x, i) => new BinaryRecord
        {
            Id = i,
            Number = ((i + offset) * 10).ToString("00000000"),
            HexCodes = x.Select(x => x.ToString("X2")).ToArray(),
            Dump = GetDumpFromHexCodes(x, sb16),
        }).ToList();
        return list;
    }

    public long GetFileCountLines(string filename)
    {
        var (exists, size) = _readerService.GetFileInfo(filename);
        return exists ? size : 0;
    }

    private string GetDumpFromHexCodes(byte[] bytes, StringBuilder sb)
    {
        var chars = _encoding.GetString(bytes).ToCharArray();
        sb.Clear();
        foreach (var ch in chars)
        {
            if (char.IsControl(ch))
            {
                sb.Append('.');
            }
            else
            {
                sb.Append(ch);
            }
        }
        return sb.ToString();
    }
}
