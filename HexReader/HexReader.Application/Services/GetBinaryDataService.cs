﻿namespace HexReader.CoreApplication.Services;

public class GetBinaryDataService : IGetDataService
{
    private readonly IFileReaderHelper _readerService;
    private readonly Encoding _encoding;
    public GetBinaryDataService(IFileReaderHelper readerService)
    {
        _readerService = readerService;
        _encoding = Encoding.GetEncoding(1251);
    }

    public IEnumerable<BinaryRecord> GetLinesDataFromFile(string filename, int offset)
    {
        var lines = _readerService.ReadBinaryLinesWithOffset(filename, offset - 20, 60);
        var sb16 = new StringBuilder(16);
        var list = lines.Where(x => x is { }).Select((x, i) => new BinaryRecord
        {
            Id = i,
            Number = i * 10,
            HexCodes = x,
            Dump = GetDumpFromHexCodes(x, sb16),
        }).ToList();
        return list;
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